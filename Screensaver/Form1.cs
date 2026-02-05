namespace Screensaver;

public partial class Form1 : Form
{
    private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 20 };
    public Form1()
    {
        InitializeComponent();
        SetupForm();

        timer.Tick += (s, e) => UpdateSnowflakes();
        this.Paint += (s, e) => DrawScene(e.Graphics);

        this.KeyDown += (s, e) => Close();
        this.MouseClick += (s, e) => Close();
    }

    private void SetupForm()
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.WindowState = FormWindowState.Maximized;
        this.BackColor = Color.Black;
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    private void UpdateSnowflakes()
    {
        
    }

    private void DrawScene(Graphics graphics)
    {
        
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        timer.Stop();
        base.OnFormClosing(e);
    }
}
