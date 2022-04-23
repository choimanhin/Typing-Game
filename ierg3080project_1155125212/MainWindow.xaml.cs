using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace ierg3080project_1155125212
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        static List<string> vocablist = new List<string>() {
            "area","book","business","case","children","company","country","day","glasses","fact","family","government","group","hand",
            "home","job","life","many","male","money","month","mother","father","night","number","part","people","place","point","problem",
            "program","question","right","room","school","state","story","student","study","system","thing","time","water","highway","weekend",
            "female","word","work","world","year"
        };
        //TypingGame currentGame;
        newGame currentGame;
        whentimepass currentGameTimePass;
        textchange currentGameTextChange;
        
        public static class choosedifficulty
        {
            public delegate int chosendifficulty();
            public static int choose(chosendifficulty difficulty)
            {
                return difficulty();
            }
        }
        public static class alldifficulty
        {
            public static int easy() { return 4; }
            public static int normal() { return 2; }
            public static int hell() { return 1; }

        }
        private void startbuttoneasy_Click(object sender, RoutedEventArgs e)
        {
            startbuttoneasy.Visibility = startbuttonnormal.Visibility = startbuttonhell.Visibility = Visibility.Collapsed;
            line1.Visibility = timestr.Visibility = timeval.Visibility = life.Visibility = lifestr.Visibility = score.Visibility = titlebutton.Visibility = stopbutton.Visibility = Visibility.Visible;
            lifestr.Foreground = timestr.Foreground = Brushes.DarkBlue;
            if (currentGame != null)
            {
                currentGame.Reset();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                dispatcherTimer.Stop();
            }
            currentGame = new newGame(timeval, canvas1, life, score, showgameover, typehere, vocablist, choosedifficulty.choose(alldifficulty.easy));
            currentGameTimePass=new whentimepass(currentGame);
            currentGameTextChange = new textchange(currentGame);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }
        private void startbuttonnormal_Click(object sender, RoutedEventArgs e)
        {
            startbuttoneasy.Visibility = startbuttonnormal.Visibility = startbuttonhell.Visibility = Visibility.Collapsed;
            line1.Visibility = timestr.Visibility = timeval.Visibility = life.Visibility = lifestr.Visibility = score.Visibility = titlebutton.Visibility = stopbutton.Visibility = Visibility.Visible;
            lifestr.Foreground = timestr.Foreground = Brushes.DarkBlue;
            if (currentGame != null)
            {
                currentGame.Reset();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                dispatcherTimer.Stop();
            }
            currentGame = new newGame(timeval, canvas1, life, score, showgameover, typehere, vocablist, choosedifficulty.choose(alldifficulty.normal));
            currentGameTimePass = new whentimepass(currentGame);
            currentGameTextChange = new textchange(currentGame);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }
        private void startbuttonhell_Click(object sender, RoutedEventArgs e)
        {
            startbuttoneasy.Visibility = startbuttonnormal.Visibility = startbuttonhell.Visibility = Visibility.Collapsed;
            line1.Visibility = timestr.Visibility = timeval.Visibility = life.Visibility = lifestr.Visibility = score.Visibility = titlebutton.Visibility = stopbutton.Visibility = Visibility.Visible;
            lifestr.Foreground = timestr.Foreground = Brushes.DarkBlue;
            if (currentGame != null)
            {
                currentGame.Reset();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                dispatcherTimer.Stop();
            }           
            currentGame = new newGame(timeval, canvas1, life, score, showgameover, typehere, vocablist, choosedifficulty.choose(alldifficulty.hell));
            currentGameTimePass = new whentimepass(currentGame);
            currentGameTextChange = new textchange(currentGame);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        private void stopbutton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
            showgameover.Text = "GAME OVER";
            showgameover.Foreground = Brushes.Red;            
        }
        private void titlebutton_Click(object sender, RoutedEventArgs e)
        {
            startbuttoneasy.Visibility = startbuttonnormal.Visibility = startbuttonhell.Visibility = Visibility.Visible;
            line1.Visibility = timestr.Visibility = timeval.Visibility = life.Visibility = lifestr.Visibility = score.Visibility = titlebutton.Visibility = stopbutton.Visibility = Visibility.Collapsed;
            canvas1.Background = Brushes.DarkGray;
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
            showgameover.Text = "Typing Game";
            showgameover.Foreground = Brushes.Black;
            if (currentGame != null)
            {
                currentGame.Reset();

            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (currentGame != null)
            {
                bool gameend = false;                
                currentGameTimePass.textblockfall(currentGame.textblocklist,currentGame);
                timeval.Text = ((currentGame.rtime)-(currentGame.timepassed)) + " s";
                currentGameTimePass.hpchange(currentGame.textblocklist);
                gameend=currentGameTimePass.gameover_time(currentGame);
                life.Text = currentGameTimePass.life.ToString();
            }
        }

        private void typehere_TextChanged(object sender, TextChangedEventArgs e) ///WHEN USER TYPES WORDS 
        {
            if (currentGame != null)
            {
                bool gameend=false;
                
                gameend=currentGameTimePass.gameover_time(currentGame);
                if (gameend == false){ 
                    currentGameTextChange.clearfallingtext(currentGame); 
                }
                score.Text = "Score: "+currentGameTextChange.returnScore();
            }
        }


    }
    
    public class newGame {
        public int timepassed;
        public int rtime;
        public int life { get; private set; }
        public int fixedlife { get; private set; }
        public int difficulty { get; private set; }
        public TextBlock timebox, scorebox, hpbox, gameover;
        public TextBox typingbox;
        public List<TextBlock> textblocklist = new List<TextBlock>();
        public  Canvas thiscanvas;        
        public  List<string> vocablist;
        public newGame(TextBlock tBox, Canvas mycanvas, TextBlock hpB, TextBlock scBox, TextBlock gameoverbox, TextBox type, List<string> l,int diffi ) {
            difficulty = diffi;
            rtime = 60;
            timepassed = 0;
            thiscanvas = mycanvas;
            vocablist = l;
            timebox = tBox;
            hpbox = hpB;
            scorebox = scBox;
            scorebox.Text = "Score: 0";
            gameover = gameoverbox;
            gameover.Text = "";
            typingbox = type;            
            life = difficulty;
            fixedlife = difficulty;
            thiscanvas.Background = Brushes.DarkGreen;
            typingbox.Text = "";
            typingbox.Focus();
            typingbox.CaretBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
        public void Reset() 
        {
            foreach (TextBlock tblock in textblocklist)
            {
                thiscanvas.Children.Remove(tblock);
            }
            hpbox.Text = "";
            typingbox.Text = "";
        }
    }
    public class whentimepass {
        private static Random rnd;
        public int rtime { get; private set; }
        public int life { get; private set; } 
        private int difficulty, fixedlife;
        private Canvas thiscanvas;
        private TextBlock timebox, lifebox, gameoverbox;
        private static List<string> vocablist;
        public bool gameend { get; private set; }     
        public whentimepass(newGame ng) { 
            rtime = ng.rtime;
            life = ng.life;
            fixedlife = ng.fixedlife;
            difficulty = ng.difficulty;
            timebox = ng.timebox;
            lifebox = ng.hpbox;
            gameoverbox = ng.gameover;
            vocablist = ng.vocablist ;
            rnd = new Random();
            gameend = false;
            thiscanvas=ng.thiscanvas;
        }
        public void createfallingtextblocks(List<TextBlock> textblockList) {
            TextBlock wordboxnew = new TextBlock();
            wordboxnew.Text = vocablist[rnd.Next(1, 50)];
            wordboxnew.FontSize = 18;
            if (wordboxnew.Text.Length <= 3) { wordboxnew.Foreground = Brushes.Lime; }
            if ((wordboxnew.Text.Length > 3) && (wordboxnew.Text.Length < 7)) { wordboxnew.Foreground = Brushes.Blue; }
            if (wordboxnew.Text.Length >= 7) { wordboxnew.Foreground = Brushes.DarkRed; }
            thiscanvas.Children.Add(wordboxnew);
            wordboxnew.Height = 35;
            Canvas.SetLeft(wordboxnew, (rnd.Next(1, 5)) * 100);
            Canvas.SetTop(wordboxnew, 0);
            textblockList.Add(wordboxnew);
        }
        public void textblockfall(List<TextBlock> textblockList, newGame ng) {
            if (gameend == true) { return; }
            if (((ng.rtime - ng.timepassed) % difficulty) == 0) { createfallingtextblocks(textblockList); }
            gameoverbox.Text = "";
            ng.timepassed += 1;            
            foreach (TextBlock tblock in textblockList)
            {
                Canvas.SetTop(tblock, Canvas.GetTop(tblock) + 30);
                if ((Canvas.GetTop(tblock) + (tblock.Height * 2) > tblock.Height + Application.Current.MainWindow.Height) && (tblock.Text.Length > 2))
                {
                    tblock.Foreground = Brushes.Red;
                }
            }
        }
        public void hpchange(List<TextBlock> textblockList) {
            if (gameend == true) { return; }
            int count = 0;
            foreach (TextBlock tblock in textblockList)
            {
                if ((Canvas.GetTop(tblock) + (tblock.Height * 2) > tblock.Height + Application.Current.MainWindow.Height) && (tblock.Text != ""))
                {
                    count += 1;
                }
            }
            if (life == (fixedlife - count))
            {
                if (life > (fixedlife / 2)) { thiscanvas.Background = Brushes.DarkGreen; }
                if ((life <= (fixedlife / 2)) && (life > 1)) { thiscanvas.Background = Brushes.DarkOrange; }
                if (life <= 1) { thiscanvas.Background = Brushes.Purple; }
            }
            else
            {
                thiscanvas.Background = Brushes.Pink;
                life = fixedlife - count;
            }
            if (life > (fixedlife / 2)) { lifebox.Foreground = Brushes.Lime; }
            if ((life <= (fixedlife / 2)) && (life > 1)) { lifebox.Foreground = Brushes.Orange; }
            if (life <= 1) { lifebox.Foreground = Brushes.Red; }
        }
        public bool gameover_time(newGame ng) {
            if ((ng.rtime==ng.timepassed) || (life == 0))
            {
                gameoverbox.Text = "GAME OVER";
                gameend = true;
                gameoverbox.Foreground = Brushes.Red;
                ng.typingbox.Clear();
                return true; 
            }
            else
            {
                if (rtime > 35) { timebox.Foreground = Brushes.Lime; }
                if ((rtime <= 35) && (rtime > 15)) { timebox.Foreground = Brushes.Orange; }
                if (rtime <= 15) { timebox.Foreground = Brushes.Red; }
                lifebox.Text = life.ToString();
                return false;
            }
        }
    }
    public class textchange{
        private int rtime;
        private int finishedwords;
        private int resultscore;
        private TextBlock chosentextblock;
        private TextBox typingbox;
        private List<TextBlock> textblocklist;
        public textchange(newGame ng) {   
        rtime = (ng.rtime - ng.timepassed);            
            typingbox = ng.typingbox;
            textblocklist = ng.textblocklist;
            finishedwords = 0;
            resultscore = 0;            
        }
        public void clearfallingtext(newGame ng) {
            if (ng.timepassed <1)   return; 
            foreach (TextBlock b in textblocklist)
            {
                if (b.Text.Length>2)
                {
                    chosentextblock = b;
                    chosentextblock.Foreground = Brushes.Black;
                    break;
                }
            }
            if ((chosentextblock.Text) == (typingbox.Text))
            {
                chosentextblock.Text = "";                
                typingbox.Text = "";
            }           
        }
        public int returnScore() {
            finishedwords = 0;
            foreach (TextBlock tblock in textblocklist)
            {
                if (tblock.Text.Length < 2) { finishedwords += 1; }
            }
            resultscore = finishedwords * 10;            
            return resultscore;
        }  
    }
}
