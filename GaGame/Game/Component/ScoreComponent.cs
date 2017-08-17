/*
 * User: Eelco
 * Date: 5/16/2017
 * Time: 9:05 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;
using GaGame;

public class ScoreComponent : ImageComponent
{
    private uint _score = 0;

	public ScoreComponent()
	{
        _image = Image.FromFile("digits.png");
	}

    public override void Render(Graphics graphics, Vec2 pos)
    {
        int digits = 2;
        string score = "000" + _score.ToString();

        for (int d = 0; d < digits; d++)
        { // 3 digits left to right
            int digit = score[score.Length - digits + d] - 48; // '0' => 0 etc
            Rectangle rect = new Rectangle(digit * _image.Width / 10, 0, _image.Width / 10, _image.Height);
            graphics.DrawImage(_image, Position.X + d * _image.Width / 10, Position.Y, rect, GraphicsUnit.Pixel);
        }
    }

    public void IncScore()
    {
        _score++;
    }

    public uint Score
    {
        get { return _score; }
    }
}

