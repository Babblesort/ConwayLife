using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ConwayLife.Domain;

namespace ConwayLife.UI
{
    public partial class LifeGameForm : Form
    {
        private readonly GameRunOptions _options;
        private readonly PlayField _field;
        private readonly LifeRules _rules;
        private LifeGame _game;
        private RunState _runState;

        private delegate void SetGenerationTextCallback(string text);
        private delegate void SetGamePanelCellsCallback(List<bool> cells);

        public LifeGameForm()
        {
            InitializeComponent();

            var tip = new ToolTip();
            _options = new GameRunOptions();
            tip.SetToolTip(numGenerations, "Maximum number of generations for run");
            numGenerations.Minimum = GameRunOptions.MinGenerations;
            numGenerations.Maximum = GameRunOptions.MaxGenerations;
            numGenerations.DataBindings.Add(new Binding("Value", _options, "AllowedGenerations", true, DataSourceUpdateMode.OnPropertyChanged));

            tip.SetToolTip(numDelay, "Milliseconds of delay between generations");
            numDelay.Minimum = GameRunOptions.MinDelayMilliseconds;
            numDelay.Maximum = GameRunOptions.MaxDelayMilliseconds;
            numDelay.DataBindings.Add(new Binding("Value", _options, "DelayStepMilliseconds", true, DataSourceUpdateMode.OnPropertyChanged));

            _field = new PlayField();
            _field.PlayFieldSizeChanged += field_PlayFieldSizeChanged;
            _field.Rows = 50;
            _field.Cols = 50;

            tip.SetToolTip(numRows, "Number of rows on the play field");
            numRows.Minimum = PlayField.MinSize;
            numRows.Maximum = PlayField.MaxSize;
            numRows.DataBindings.Add(new Binding("Value", _field, "Rows", true, DataSourceUpdateMode.OnPropertyChanged));

            tip.SetToolTip(numCols, "Number of columns on the play field");
            numCols.Minimum = PlayField.MinSize;
            numCols.Maximum = PlayField.MaxSize;
            numCols.DataBindings.Add(new Binding("Value", _field, "Cols", true, DataSourceUpdateMode.OnPropertyChanged));

            _rules = new LifeRules();
            for (var i = LifeRules.MinNeighborCount; i <= LifeRules.MaxNeighborCount; i++)
            {
                chklstSurviveCounts.Items.Add($"{i} Neighbors");
                chklistBornCounts.Items.Add($"{i} Neighbors");
            }

            foreach(var i in _rules.SurvivalNeighborCounts) {
                chklstSurviveCounts.SetItemChecked(i, true);
            }
            foreach (var i in _rules.BirthNeighborCounts)
            {
                chklistBornCounts.SetItemChecked(i, true);
            }

            btnRunGame.Click += btnRunGame_Click;
            btnStepGame.Click += btnStepGame_Click;
            btnPauseGame.Click += btnPauseGame_Click;
            btnClearGame.Click += btnClearGame_Click;
            btnExit.Click += btnExit_Click;

            chklstSurviveCounts.ItemCheck += chklstSurviveCounts_ItemCheck;
            chklistBornCounts.ItemCheck += chklistBornCounts_ItemCheck;

            bwGame.DoWork += bwGame_DoWork;
            bwGame.RunWorkerCompleted += bwGame_RunWorkerCompleted;

            _runState = RunState.Idle;
            SetUiForGameState();
        }

        private void btnRunGame_Click(object sender, EventArgs e)
        {
            if (_game == null)
            {
                _game = new LifeGame(_rules, _field);
                pnlField.RowsCount = _field.Rows;
                pnlField.ColsCount = _field.Cols;
                _game.GenerationResolvedHandler += GameGenerationResolvedHandler;
            }
            _runState = RunState.Continuous;
            SetUiForGameState();
            bwGame.RunWorkerAsync(EventArgs.Empty);
        }

        private void btnStepGame_Click(object sender, EventArgs e)
        {
            if (_game == null)
            {
                _game = new LifeGame(_rules, _field);
                pnlField.RowsCount = _field.Rows;
                pnlField.ColsCount = _field.Cols;
                _game.GenerationResolvedHandler += GameGenerationResolvedHandler;
            }
            _runState = RunState.Step;
            SetUiForGameState();
            _game.ResolveNextGeneration();
        }

        private void TerminateGame()
        {
            if (_game != null)
            {
                _game.GenerationResolvedHandler -= GameGenerationResolvedHandler;
                _game = null;
            }
            _runState = RunState.Idle;
            SetUiForGameState();
        }

        private void btnClearGame_Click(object sender, EventArgs e)
        {
            ClearBoard();
            TerminateGame();
        }

        private void btnPauseGame_Click(object sender, EventArgs e)
        {
            bwGame.CancelAsync();
        }

        private static void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void field_PlayFieldSizeChanged(object sender, PlayFieldSizeChangedEventArgs e)
        {
            ClearBoard();
            pnlField.RowsCount = e.Rows;
            pnlField.ColsCount = e.Cols;
            pnlField.Refresh();
        }

        private void chklistBornCounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                _rules.BirthNeighborCounts.Add(e.Index);
            }
            else
            {
                _rules.BirthNeighborCounts.Remove(e.Index);
            }
        }

        private void chklstSurviveCounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                _rules.SurvivalNeighborCounts.Add(e.Index);
            }
            else
            {
                _rules.SurvivalNeighborCounts.Remove(e.Index);
            }
        }

        private void bwGame_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            RunGameContinuous(worker, e);
        }

        private void bwGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("There was an error running the game.", "Error", MessageBoxButtons.OK);
                TerminateGame();
            }
            if (e.Cancelled)
            {
                // Game paused
                _runState = RunState.Step;
                SetUiForGameState();
            }
            else
            {
                TerminateGame();
                _runState = RunState.Step;
                SetUiForGameState();
            }
        }

        public void RunGameContinuous(BackgroundWorker workerThread, DoWorkEventArgs e)
        {
            var halted = false;

            while (!halted)
            {
                if (workerThread.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                Thread.Sleep(_options.DelayStepMilliseconds);
                _game.ResolveNextGeneration();

                if (_options.HaltOnExtinction && _game.Extinction || _game.Generation >= _options.AllowedGenerations)
                {
                    halted = true;
                }
            }

        }

        private void SetUiForGameState()
        {
            switch (_runState) {

                case RunState.Idle:
                {
                    btnRunGame.Enabled = true;
                    btnStepGame.Enabled = true;
                    btnPauseGame.Enabled = false;
                    btnClearGame.Enabled = true;
                    SetGameOptionControlsAvailable(available: true);
                    btnStepGame.Focus();
                    break;
                }
                case RunState.Step: 
                {
                    btnRunGame.Enabled = true;
                    btnStepGame.Enabled = true;
                    btnPauseGame.Enabled = false;
                    btnClearGame.Enabled = true;
                    SetGameOptionControlsAvailable(available: false);
                    btnStepGame.Focus();
                    break;
                }
                case RunState.Continuous:
                {
                    btnRunGame.Enabled = false;
                    btnStepGame.Enabled = false;
                    btnPauseGame.Enabled = true;
                    btnClearGame.Enabled = false;
                    btnPauseGame.Focus();
                    SetGameOptionControlsAvailable(available: false);
                    break;
                }
            }
        }

        private void SetGameOptionControlsAvailable(bool available)
        {
            chklstSurviveCounts.Enabled = available;
            chklistBornCounts.Enabled = available;
            numRows.Enabled = available;
            numCols.Enabled = available;
            numGenerations.Enabled = available;
            numDelay.Enabled = available;
        }

        private void ClearBoard()
        {
            pnlField.ClearBoard();
            lblGeneration.Text = "n/a";
        }

        private void GameGenerationResolvedHandler(object sender, GenerationResolvedEventArgs e)
        {
            SetGenerationText(e.Generation.ToString("N0"));
            SetGamePanelCells(e.CellStates);
        }

        private void SetGenerationText(string value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SetGenerationTextCallback(SetGenerationText), value);
            }
            else
            {
                lblGeneration.Text = value;
            }
        }

        private void SetGamePanelCells(List<bool> cells)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SetGamePanelCellsCallback(SetGamePanelCells), cells);
            }
            else
            {
                pnlField.CellStates = cells;
                pnlField.Refresh();
            }
        }
    }
}
