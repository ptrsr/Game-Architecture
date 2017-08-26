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

    protected override void Start()
    {
        _image = Image.FromFile("digits.png");

        //setup event handling
        Events.score += IncScore;
        Events.reset += Reset;
    }

    public override void OnRender(Graphics graphics, Vec2 pos)
    {
        int digits = 2;
        string score = "000" + _score.ToString();

        for (int d = 0; d < digits; d++)
        { // 3 digits left to right
            int digit = score[score.Length - digits + d] - 48; // '0' => 0 etc
            Rectangle rect = new Rectangle(digit * _image.Width / 10, 0, _image.Width / 10, _image.Height);
            graphics.DrawImage(_image, (Position.X + d * _image.Width / 10) - rect.Width, Position.Y - rect.Height / 2, rect, GraphicsUnit.Pixel);
        }
    }

    private void IncScore(Tags.Tag tag)
    {
        if (tag == Parent.Tag)
            _score++;

        Debug.Assert(_score >= 0 && _score < 100);
    }

    private void Reset(bool hard)
    {
        if (hard)
            _score = 0;
    }

    public uint Score
    {
        get { return _score; }
    }
}

