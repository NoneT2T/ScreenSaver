namespace Screensaver;

/// <summary>
/// Код дизайнера для формы ScreensaverForm.
/// </summary>
partial class ScreensaverForm
{
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Timer animationTimer;

    /// <summary>
    /// Освобождение ресурсов.
    /// </summary>
    /// <param name="disposing">true, если управляемые ресурсы должны быть освобождены; иначе false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Код, создаваемый конструктором форм Windows

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        animationTimer = new System.Windows.Forms.Timer(components);
        SuspendLayout();
        //
        // animationTimer
        //
        animationTimer.Interval = 20;
        animationTimer.Tick += AnimationTimer_Tick;
        //
        // ScreensaverForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Black;
        ClientSize = new System.Drawing.Size(1940, 1100);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        Text = "Screensaver";
        TopMost = true;
        WindowState = System.Windows.Forms.FormWindowState.Maximized;
        FormClosing += ScreensaverForm_FormClosing;
        Paint += ScreensaverForm_Paint;
        KeyDown += ScreensaverForm_KeyDown;
        MouseClick += ScreensaverForm_MouseClick;
        ResumeLayout(false);
    }

    #endregion

    /// <summary>
    /// Обработчик события Tick таймера анимации.
    /// </summary>
    private void AnimationTimer_Tick(object sender, EventArgs e)
    {
        UpdateSnowflakes();
    }

    /// <summary>
    /// Обработчик события Paint (отрисовка).
    /// </summary>
    private void ScreensaverForm_Paint(object sender, PaintEventArgs e)
    {
        DrawScene(e.Graphics);
    }

    /// <summary>
    /// Обработчик события KeyDown (нажатие клавиши) - закрывает заставку.
    /// </summary>
    private void ScreensaverForm_KeyDown(object sender, KeyEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Обработчик события MouseClick (клик мыши) - закрывает заставку.
    /// </summary>
    private void ScreensaverForm_MouseClick(object sender, MouseEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Обработчик события FormClosing (закрытие формы) - очистка ресурсов.
    /// </summary>
    private void ScreensaverForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        CleanupResources();
    }
}
