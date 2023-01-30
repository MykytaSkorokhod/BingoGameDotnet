using BingoGame.Client.ConsoleUI;
using BingoGame.Client.Resources;
using BingoGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoGame.Client.Output
{
    internal class BingoMenu
    {
        const string startKey = "start";
        const string optionsKey = "options";
        const string exitKey = "exit";
        const string availableNumberRangeKey = "availableNumberRange";
        const string tableMeasureKey = "tableMeasure";
        const string winStrategyKey = "winStrategy";

        Dictionary<string, string> menuVariation = new Dictionary<string, string>(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(startKey, $"1) {Localization.startMenu}"),
            //new KeyValuePair<string, string>(optionsKey, $"2) {Localization.optionsMenu}"),
            new KeyValuePair<string, string>(exitKey, $"3) {Localization.exitMenu}"),
        });

        Dictionary<string, string> optionsVariation = new Dictionary<string, string>(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(availableNumberRangeKey, $"1) {Localization.availableNumberRange}"),
            new KeyValuePair<string, string>(tableMeasureKey, $"2) {Localization.tableMeasure}"),
            new KeyValuePair<string, string>(winStrategyKey, $"3) {Localization.winStrategy}"),
        });

        /// <summary>
        /// Start game, chose options and exit game.
        /// Now Options disabled
        /// </summary>
        public BingoMenu()
        {

        }

        #region API

        public bool IsMenuActive { get; set; } = true;

        public MenuState State { get; set; }

        /// <summary>
        /// Get options which installed in menu. Now only default
        /// </summary>
        /// <returns></returns>
        public BingoGameOptions GetBingoGameOptions()
        {
            var options = new BingoGameOptions
            {

            };

            return options;
        }

        /// <summary>
        /// Run menu. Now, start game indicates if menu closed.
        /// </summary>
        public void Run()
        {
            while (IsMenuActive)
            {
                UpdateFrame();
                var result = Console.ReadLine();
                int integerResult = 0;

                if (result == null || !int.TryParse(result, out integerResult))
                    continue;

                switch (State)
                {
                    case MenuState.Initial:
                        if (integerResult == 0)
                            continue;
                        if (integerResult == 1)
                            IsMenuActive = false;
                        if (integerResult == 3)
                            Environment.Exit(0);
                        break;
                    case MenuState.Start:
                        break;
                    case MenuState.Options:
                        break;
                    case MenuState.Exit:
                        break;
                    case MenuState.NumberRangeChosing:
                        break;
                    case MenuState.TableMeasureChosing:
                        break;
                    case MenuState.WinStrategyChosing:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region UI

        void UpdateFrame()
        {

            switch (State)
            {
                case MenuState.Initial:
                    Console.Clear();
                    Console.Write($"\n{Localization.welcomeMessage}\n");
                    foreach (var item in menuVariation)
                    {
                        Console.Write(item.Value + "\n");
                    }
                    Console.Write($"\n{Localization.choseMessage} ");
                    break;
                case MenuState.Start:
                    break;
                case MenuState.Options:
                    //Console.Clear();
                    //foreach (var item in optionsVariation)
                    //{
                    //    Console.Write(item.Value + "\n");
                    //    Console.Write($"\n{Localization.choseMessage} ");
                    //}
                    break;
                case MenuState.Exit:
                    break;
                case MenuState.NumberRangeChosing:
                    break;
                case MenuState.TableMeasureChosing:
                    break;
                case MenuState.WinStrategyChosing:
                    break;
                default:
                    break;
            }
        }

        #endregion
    }

    enum MenuState
    {
        Initial = 0,
        Start = 1,
        Options = 2,
        Exit = 3,
        NumberRangeChosing = 4,
        TableMeasureChosing = 5,
        WinStrategyChosing = 6,
    }
}
