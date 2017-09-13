using ConwayLife.Domain;
using ConwayLife.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace ConwayLifeWinForms
{
    public partial class frmLifeGame : Form
    {
        GameRunOptions options;
        PlayField field;
        LifeRules rules;
        LifeGame game;

        ToolTip tip;
        RunState runState;
        delegate void SetGenerationTextCallback(string text);
        delegate void SetGamePanelCellsCallback(List<bool> cells);

        public frmLifeGame()
        {
            InitializeComponent();

            tip = new ToolTip();
            options = new GameRunOptions();
            tip.SetToolTip(numGenerations, "Maximum number of generations for run");
            numGenerations.Minimum = GameRunOptions.MinGenerations;
            numGenerations.Maximum = GameRunOptions.MaxGenerations;
            numGenerations.DataBindings.Add(new Binding("Value", options, "AllowedGenerations", true, DataSourceUpdateMode.OnPropertyChanged));

            tip.SetToolTip(numDelay, "Milliseconds of delay between generations");
            numDelay.Minimum = GameRunOptions.MinDelayMilliseconds;
            numDelay.Maximum = GameRunOptions.MaxDelayMilliseconds;
            numDelay.DataBindings.Add(new Binding("Value", options, "DelayStepMilliseconds", true, DataSourceUpdateMode.OnPropertyChanged));

            field = new PlayField();
            field.PlayFieldSizeChanged += field_PlayFieldSizeChanged;
            field.Rows = 50;
            field.Cols = 50;

            tip.SetToolTip(numRows, "Number of rows on the play field");
            numRows.Minimum = PlayField.MinSize;
            numRows.Maximum = PlayField.MaxSize;
            numRows.DataBindings.Add(new Binding("Value", field, "Rows", true, DataSourceUpdateMode.OnPropertyChanged));

            tip.SetToolTip(numCols, "Number of columns on the play field");
            numCols.Minimum = PlayField.MinSize;
            numCols.Maximum = PlayField.MaxSize;
            numCols.DataBindings.Add(new Binding("Value", field, "Cols", true, DataSourceUpdateMode.OnPropertyChanged));

            rules = new LifeRules();
            for (var i = LifeRules.MinNeighborCount; i <= LifeRules.MaxNeighborCount; i++)
            {
                chklstSurviveCounts.Items.Add(string.Format("{0} Neighbors", i));
                chklistBornCounts.Items.Add(string.Format("{0} Neighbors", i));
            }

            foreach(var i in rules.SurvivalNeighborCounts) {
                chklstSurviveCounts.SetItemChecked(i, true);
            }
            foreach (var i in rules.BirthNeighborCounts)
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

            runState = RunState.Idle;
            SetUIForGameState();
        }

        void btnRunGame_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                game = new LifeGame(rules, field);
                pnlField.RowsCount = field.Rows;
                pnlField.ColsCount = field.Cols;
                game.GenerationResolvedHandler += GameGenerationResolvedHandler;
            }
            runState = RunState.Continuous;
            SetUIForGameState();
            bwGame.RunWorkerAsync(EventArgs.Empty);
        }

        void btnStepGame_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                game = new LifeGame(rules, field);
                pnlField.RowsCount = field.Rows;
                pnlField.ColsCount = field.Cols;
                game.GenerationResolvedHandler += GameGenerationResolvedHandler;
            }
            runState = RunState.Step;
            SetUIForGameState();
            game.ResolveNextGeneration();
        }

        void TerminateGame()
        {
            if (game != null)
            {
                game.GenerationResolvedHandler -= GameGenerationResolvedHandler;
                game = null;
            }
            runState = RunState.Idle;
            SetUIForGameState();
        }

        void btnClearGame_Click(object sender, EventArgs e)
        {
            ClearBoard();
            TerminateGame();
        }

        void btnPauseGame_Click(object sender, EventArgs e)
        {
            bwGame.CancelAsync();
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        void field_PlayFieldSizeChanged(object sender, PlayFieldSizeChangedEventArgs e)
        {
            ClearBoard();
            pnlField.RowsCount = e.Rows;
            pnlField.ColsCount = e.Cols;
            pnlField.Refresh();
        }

        void chklistBornCounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                rules.BirthNeighborCounts.Add(e.Index);
            }
            else
            {
                rules.BirthNeighborCounts.Remove(e.Index);
            }
        }

        void chklstSurviveCounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                rules.SurvivalNeighborCounts.Add(e.Index);
            }
            else
            {
                rules.SurvivalNeighborCounts.Remove(e.Index);
            }
        }

        void bwGame_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            RunGameContinuous(worker, e);
        }

        void bwGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("There was an error running the game.", "Error", MessageBoxButtons.OK);
                TerminateGame();
            }
            if (e.Cancelled)
            {
                // Game paused
                runState = RunState.Step;
                SetUIForGameState();
            }
            else
            {
                TerminateGame();
                runState = RunState.Step;
                SetUIForGameState();
            }
        }

        public void RunGameContinuous(BackgroundWorker workerThread, DoWorkEventArgs e)
        {

            bool halted = false;

            while (!halted)
            {
                if (workerThread.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(options.DelayStepMilliseconds);
                    game.ResolveNextGeneration();

                    if (options.HaltOnExtinction && game.Extinction || game.Generation >= options.AllowedGenerations)
                    {
                        halted = true;
                    }
                }
            }

        }

        void SetUIForGameState()
        {
            switch (runState) {

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

        void SetGameOptionControlsAvailable(bool available)
        {
            chklstSurviveCounts.Enabled = available;
            chklistBornCounts.Enabled = available;
            numRows.Enabled = available;
            numCols.Enabled = available;
            numGenerations.Enabled = available;
            numDelay.Enabled = available;
        }

        void ClearBoard()
        {
            pnlField.ClearBoard();
            lblGeneration.Text = "n/a";
        }

        void GameGenerationResolvedHandler(object sender, GenerationResolvedEventArgs e)
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
