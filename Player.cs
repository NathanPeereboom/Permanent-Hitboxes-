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
    class Player
    {
        int player;
        Key leftButton;
        Key rightButton;
        Key attack1Button;
        Canvas canvas;
        double xPos;
        double yPos;
        int action;
        bool lag;
        int animation;
        double knockbackAngle;
        int knockbackSpeed;
        int knockbackDuration;
        int hitstun;
        int direction;
        bool falling;
        int health;
        Rectangle playerSprite = new Rectangle();
        Hitbox hitbox = new Hitbox();
        Rect hitboxRect = new Rect();

        public Player(int p, Key leftBtn, Key rightBtn, Key atk1Btn)
        {
            player = p;
            leftButton = leftBtn;
            rightButton = rightBtn;
            attack1Button = atk1Btn;
            playerSprite.Width = 64;
            playerSprite.Height = 64;
            yPos = 300;
            if (player == 1)
            {
                playerSprite.Fill = Brushes.Red;
                xPos = 100;
                direction = -1;
            }
            if (player == 2)
            {
                playerSprite.Fill = Brushes.Blue;
                xPos = 600;
                direction = 1;
            }
            health = 100;
        }

        public void generate(Canvas c)
        {
            canvas = c;
            Canvas.SetLeft(playerSprite, xPos);
            Canvas.SetTop(playerSprite, yPos);
            canvas.Children.Add(playerSprite);
        }

        public void move(double opponentX)
        {
            if (!lag && hitstun == 0)
            {
                if (Keyboard.IsKeyDown(leftButton))
                {
                    if (xPos < (opponentX + 64) && player == 2);
                    else
                    {
                        xPos -= 8;
                    }
                }
                if (Keyboard.IsKeyDown(rightButton))
                {
                    if (xPos + 64 > opponentX && player == 1);
                    else
                    {
                        xPos += 8;
                    }
                }
                if (Keyboard.IsKeyDown(attack1Button))
                {
                    action = 1;
                    lag = true;
                    animation = 9;
                }
            }
        }

        public void animate()
        {
            if (knockbackDuration > 0)
            {
                xPos += 10 * (Math.Cos(knockbackAngle) * direction);
                yPos -= 10 * Math.Sin(knockbackAngle);
            }


            Canvas.SetLeft(playerSprite, xPos);
            Canvas.SetTop(playerSprite, yPos);
            if (action == 1) action1();
            if (animation > 0) animation -= 1;
            if (hitstun > 0) hitstun -= 1;
            if (knockbackDuration > 0) knockbackDuration -= 1;
            if (yPos < 300) falling = true;
            if (yPos >= 300) falling = false;
            if (falling) yPos += 5;
            else yPos = 300;
        }

        public double getXPos()
        {
            return xPos;
        }

        public double getYPos()
        {
            return yPos;
        }

        public Hitbox getHitbox()
        {
            return hitbox;
        }

        public Rect getHitboxRect()
        {
            return hitboxRect;
        }

        public void action1()
        {
            if (animation == 6)
            {
                if (player == 1) hitbox.generate((int)xPos + 64, (int)yPos + 24, 24, 24, 5, 45, 2, 8, 8, canvas);
                if (player == 2) hitbox.generate((int)xPos - 24, (int)yPos + 24, 24, 24, 5, 45, 2, 8, 8, canvas);
                hitboxRect = hitbox.update(true);
            }
            if (animation == 5)
            {
                hitboxRect = hitbox.update(false);
            }
            if (animation == 0)
            {
                lag = false;
            }
        }

        public void wasHit(int damage, int ka, int ks, int kd, int hs)
        {
            health -= damage;
            if (health < 0) health = 0;
            knockbackAngle = ka;
            knockbackSpeed = ks;
            knockbackDuration = kd;
            hitstun = hs;
            animation = 0;
            hitbox.update(false);
        }

        public int getHealth()
        {
            return health;
        }
    }
}
