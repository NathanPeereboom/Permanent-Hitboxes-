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

namespace _312840Culminating
{
    class Hitbox
    {
        int xPos;
        int yPos;
        int height;
        int width;
        int damage;
        double knockbackAngle;
        int knockbackSpeed;
        int knockbackDuration;
        int hitstunDuration;
        bool displayed;
        Canvas canvas;
        Rect box;
        Rectangle boxDisplay;

        public Hitbox()
        {
            boxDisplay = new Rectangle();
        }
        public void generate(int x, int y, int h, int w, int d, int kA, int kS, int kD, int hD, Canvas c)
        {
            xPos = x;
            yPos = y;
            height = h;
            width = w;
            damage = d;
            knockbackAngle = kA;
            knockbackSpeed = kS;
            knockbackDuration = kD;
            hitstunDuration = hD;
            canvas = c;
        }

        public Rect update(bool exists)
        {
            boxDisplay.Opacity = .5;
            boxDisplay.Fill = Brushes.Purple;
            boxDisplay.Height = height;
            boxDisplay.Width = width;
            if (exists)
            {
                canvas.Children.Add(boxDisplay);
                displayed = true;
            }
            else if (displayed)
            {
                canvas.Children.Remove(boxDisplay);
                displayed = false;
            }


            Canvas.SetLeft(boxDisplay, xPos);
            Canvas.SetTop(boxDisplay, yPos);


            if (exists)
            {
                box.X = xPos;
                box.Y = yPos;
                box.Height = height;
                box.Width = width;
            }
            else
            {
                box.X = 1000;
                box.Y = 1000;
                box.Height = 0;
                box.Width = 0;
            }
            return box;
        }

        public int getDamage()
        {
            return damage;
        }

        public double getKnockbackAngle()
        {
            return knockbackAngle;
        }

        public int getKnockbackSpeed()
        {
            return knockbackSpeed;
        }

        public int getKnockbackDuration()
        {
            return knockbackDuration;
        }

        public int getHitstunDuration()
        {
            return hitstunDuration;
        }
    }
}
