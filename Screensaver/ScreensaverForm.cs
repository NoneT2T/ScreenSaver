namespace Screensaver;

/// <summary>
/// Главная форма приложения
/// </summary>
public partial class ScreensaverForm : Form
{
    private PointF[] snowflakes; // Позиция снежинок
    
    private float[] speeds; // Скорость падения
    
    private int[] sizes; // Размер снежинок
    
    private Random random; // Рандом
    
    private Image? snowflakeImage; // Изображение снежинки
    private Image? backgroundImage; // Фоновое изображение
    
    private Bitmap? backBuffer; // Буфер для отрисовки
    private Graphics? bufferGraphics; // Graphics объект буфера
    
    private const int SnowflakeCount = 150; // Количество снежинок на экране
    private const int MinInitialYOffset = 20; // Минимальное начальное смещение по Y
    private const int MaxInitialYOffset = 200; // Максимальное начальное смещение по Y
    private const int MinSnowflakeSize = 5; // Минимальный размер снежинки
    private const int MaxSnowflakeSize = 26; // Максимальный размер снежинки
    
    private const float BaseSpeed = 0.5f; // Базовая скорость падения снежинки
    private const float MaxSpeedMultiplier = 3f; // Максимальный множитель скорости для крупных снежинок
    private const float SpeedFactor = 25f; // Коэффициент для расчёта скорости на основе размера
    private const float MaxDriftFactor = 0.5f; // Боковое смещение

    /// <summary>
    /// Инициализирует новый экземпляр формы ScreensaverForm.
    /// </summary>
    public ScreensaverForm()
    {
        InitializeComponent();

        random = new Random();
        snowflakes = new PointF[SnowflakeCount];
        speeds = new float[SnowflakeCount];
        sizes = new int[SnowflakeCount];

        CreateBackBuffer();
        LoadImages();
        InitializeSnowflakes();
    }

    /// <summary>
    /// Создаёт буфер для отрисовки при изменении размера формы
    /// </summary>
    private void CreateBackBuffer()
    {
        bufferGraphics?.Dispose();
        backBuffer?.Dispose();
        
        if (Width > 0 && Height > 0)
        {
            backBuffer = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(backBuffer);
            bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
    }

    /// <summary>
    /// Загружает изображения снежинки и фона
    /// </summary>
    private void LoadImages()
    {
        try
        {
            snowflakeImage = Properties.Resources.snowflake;
        }
        catch
        {
            // Приложение продолжает работу, если ресурс снежинки не найден
        }

        try
        {
            backgroundImage = Properties.Resources.background;
        }
        catch
        {
            // Приложение продолжает работу, если ресурс фона не найден
        }
    }

    /// <summary>
    /// Инициализирует все снежинки
    /// </summary>
    private void InitializeSnowflakes()
    {
        var horizontalStep = Width / (float)SnowflakeCount;

        for (int snowflakeIndex = 0; snowflakeIndex < SnowflakeCount; snowflakeIndex++)
        {
            var xPosition = horizontalStep * snowflakeIndex + random.Next(-10, 10);
            var yPosition = -random.Next(MinInitialYOffset, MaxInitialYOffset);

            snowflakes[snowflakeIndex] = new PointF(xPosition, yPosition);

            sizes[snowflakeIndex] = random.Next(MinSnowflakeSize, MaxSnowflakeSize);
            var speedFactor = sizes[snowflakeIndex] / SpeedFactor;
            speeds[snowflakeIndex] = BaseSpeed + speedFactor * MaxSpeedMultiplier;
        }

        animationTimer.Start();
    }

/// <summary>
/// Обновляет позиции снежинок для создания эффекта
/// </summary>
private void UpdateSnowflakes()
{
    for (int snowflakeIndex = 0; snowflakeIndex < SnowflakeCount; snowflakeIndex++)
    {
        // Двигаем снежинку
        snowflakes[snowflakeIndex].Y += speeds[snowflakeIndex];
        snowflakes[snowflakeIndex].X += (float)((random.NextDouble() - 0.5) * MaxDriftFactor);

        // Проверяем, достигла ли снежинка нижней границы
        // Учитываем размер снежинки, чтобы она полностью скрывалась внизу
        if (snowflakes[snowflakeIndex].Y > Height + sizes[snowflakeIndex])
        {
            // Когда снежинка полностью ушла за нижнюю границу, сбрасываем её
            ResetSnowflake(snowflakeIndex);
        }

        // Обработка боковых границ
        if (snowflakes[snowflakeIndex].X < -sizes[snowflakeIndex])
        {
            snowflakes[snowflakeIndex].X = Width + sizes[snowflakeIndex];
        }

        if (snowflakes[snowflakeIndex].X > Width + sizes[snowflakeIndex])
        {
            snowflakes[snowflakeIndex].X = -sizes[snowflakeIndex];
        }
    }

    // Отрисовка (ваш существующий код остается без изменений)
    using (var screenGraphics = CreateGraphics())
    {
        if (bufferGraphics != null && backBuffer != null)
        {
            bufferGraphics.Clear(Color.Black);

            if (backgroundImage != null)
            {
                bufferGraphics.DrawImage(backgroundImage, 0, 0, Width, Height);
            }

            if (snowflakeImage != null)
            {
                for (int snowflakeIndex = 0; snowflakeIndex < SnowflakeCount; snowflakeIndex++)
                {
                    var currentSnowflake = snowflakes[snowflakeIndex];
                    var size = sizes[snowflakeIndex];
                    
                    // Не отрисовываем снежинки, которые уже полностью упали за границу
                    if (currentSnowflake.Y <= Height + size && currentSnowflake.Y >= -size)
                    {
                        bufferGraphics.DrawImage(snowflakeImage, currentSnowflake.X, currentSnowflake.Y, size, size);
                    }
                }
            }
            screenGraphics.DrawImageUnscaled(backBuffer, 0, 0);
        }
    }
}

    /// <summary>
    /// Сбрасывает снежинку в верхнюю часть экрана
    /// </summary>
    private void ResetSnowflake(int snowflakeIndex)
    {
        snowflakes[snowflakeIndex].X = random.Next(0, Width);
        snowflakes[snowflakeIndex].Y = -sizes[snowflakeIndex];

        var speedFactor = sizes[snowflakeIndex] / SpeedFactor;
        speeds[snowflakeIndex] = BaseSpeed + speedFactor * MaxSpeedMultiplier;
    }

    /// <summary>
    /// Отрисовывает сцену
    /// </summary>
    private void DrawScene(Graphics graphics)
    {
        if (backgroundImage != null)
        {
            graphics.DrawImage(backgroundImage, 0, 0, Width, Height);
        }

        if (snowflakeImage != null)
        {
            for (int snowflakeIndex = 0; snowflakeIndex < SnowflakeCount; snowflakeIndex++)
            {
                var currentSnowflake = snowflakes[snowflakeIndex];
                var size = sizes[snowflakeIndex];
                graphics.DrawImage(snowflakeImage, currentSnowflake.X, currentSnowflake.Y, size, size);
            }
        }
    }

    /// <summary>
    /// Освобождает ресурсы
    /// </summary>
    private void CleanupResources()
    {
        animationTimer?.Stop();
        animationTimer?.Dispose();
        snowflakeImage?.Dispose();
        backgroundImage?.Dispose();
        bufferGraphics?.Dispose();
        backBuffer?.Dispose();
    }
}
