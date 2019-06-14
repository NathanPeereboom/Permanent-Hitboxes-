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
using System.Drawing;
using System.Media;
using System.IO;
using System.Windows.Threading;

namespace _312840Culminating
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player p1 = new Player(1, Key.A, Key.D, Key.D1);
        Player p2 = new Player(2, Key.Left, Key.Right, Key.OemComma);
        DispatcherTimer gameTimer = new DispatcherTimer();
        Hitbox[] hitbox = new Hitbox[2];
        Rect[] hitboxRect = new Rect[2];
        Rect[] hurtBox = new Rect[2];
        Rectangle[] lifebar = new Rectangle[2];
        
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 17);
            p1.generate(canvas);
            p2.generate(canvas);
            gameTimer.Start();

            for (int i = 0; i < 2; i++)
            {
                hurtBox[i] = new Rect();
                hitbox[i] = new Hitbox();
                hitboxRect[i] = new Rect();
                lifebar[i] = new Rectangle();
            }

            Canvas.SetTop(lifebar[0], 20);
            Canvas.SetLeft(lifebar[0], 40);
            lifebar[0].Height = 30;
            lifebar[0].Fill = Brushes.Red;
            canvas.Children.Add(lifebar[0]);

            Canvas.SetTop(lifebar[1], 20);
            Canvas.SetRight(lifebar[1], 40);
            lifebar[1].Height = 30;
            lifebar[1].Fill = Brushes.Blue;
            canvas.Children.Add(lifebar[1]);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            p1.move(p2.getXPos());
            p2.move(p1.getXPos());
            p1.animate();
            p2.animate();
            hitbox[0] = p1.getHitbox();
            hitboxRect[0] = p1.getHitboxRect();
            hitbox[1] = p2.getHitbox();
            hitboxRect[1] = p2.getHitboxRect();
            hurtBox[0].X = p1.getXPos();
            hurtBox[0].Y = p1.getYPos();
            hurtBox[0].Width = 64;
            hurtBox[0].Height = 64;

            hurtBox[1].X = p2.getXPos();
            hurtBox[1].Y = p2.getYPos();
            hurtBox[1].Width = 64;
            hurtBox[1].Height = 64;

                if (hitboxRect[0].IntersectsWith(hurtBox[1]))
                {
                    p2.wasHit(hitbox[0].getDamage(),(int)hitbox[0].getKnockbackAngle(), hitbox[0].getKnockbackSpeed(), hitbox[0].getKnockbackDuration(), hitbox[0].getHitstunDuration());
                }
                if (hitboxRect[1].IntersectsWith(hurtBox[0]))
                {
                    p1.wasHit(hitbox[1].getDamage(),(int)hitbox[1].getKnockbackAngle(), hitbox[1].getKnockbackSpeed(), hitbox[1].getKnockbackDuration(), hitbox[1].getHitstunDuration());
                }

            lifebar[0].Width = 3 * p1.getHealth();
            lifebar[1].Width = 3 * p2.getHealth();
        }
    }
}
