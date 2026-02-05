namespace Screensaver;

public partial class Form1 : Form
{
    private PointF[] snowflakes = new PointF[100];
    private float[] speeds = new float[100];
    private int[] sizes = new int[100];
    private Random random = new Random();
    private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 20 };
    
    public Form1()
    {
        InitializeComponent();
        SetupForm();
        InitializeSnowflakes();

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

    private void InitializeSnowflakes()
    {
        float step = Width/(float) snowflakes.Length;
        int screen = this.Width;

        for (int i = 0; i < snowflakes.Length; i++)
        {
            float xpos = step * i + random.Next(-screen, screen);
            float ypos = -random.Next(-screen, screen);
            
            snowflakes[i] = new PointF(xpos, ypos);
            
            sizes[i] = random.Next(5,26);
            float speedFactor = sizes[i]/25f;
            speeds[i] = 0.5f + speedFactor * 3;
        }
        timer.Start();
    }

    private void UpdateSnowflakes()
    {
        for (int i = 0; i < snowflakes.Length; i++)
        {
            snowflakes[i].Y += speeds[i];
            snowflakes[i].X += (float)((random.NextDouble() - 0.5) * 0.5);

            if (snowflakes[i].Y > Height) ResetSnowflake(i);
            if(snowflakes[i].X < -sizes[i]) snowflakes[i].X = Width + sizes[i];
            if(snowflakes[i].X > Width + sizes[i]) snowflakes[i].X = -sizes[i];
        }
        Invalidate();
    }

    private void ResetSnowflake(int i)
    {
        snowflakes[i].X = random.Next(Width);
        snowflakes[i].Y = -sizes[i];
        
        float speedFactor = sizes[i]/25f;
        speeds[i] = 0.5f + speedFactor * 3f;
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
