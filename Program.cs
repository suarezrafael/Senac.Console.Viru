using System.Runtime.InteropServices;

const int EVENTO_CLIQUE_ESQUERDO_PRESS = 0x0002;
const int EVENTO_CLIQUE_ESQUERDO_SOLTA = 0x0004;

[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
static extern void mouse_event(int acoes, int posX, int posY, int botoes, int informacoesExtras);

[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
static extern bool SetCursorPos(int posX, int posY);

[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
static extern IntPtr GetDC(IntPtr handle);

[DllImport("gdi32.dll")]
static extern int GetDeviceCaps(IntPtr hdc, int indice);

[DllImport("user32.dll")]
static extern int ReleaseDC(IntPtr handle, IntPtr hdc);

const int RESOLUCAO_LARGURA = 8;
const int RESOLUCAO_ALTURA = 10;

Random geradorAleatorio = new Random();

Console.WriteLine("Pressione qualquer tecla para iniciar o movimento do mouse...");
Console.ReadKey();

for (int i = 1; i <= 10; i++)
{
    MoverCursorAleatoriamente();
    SimularCliqueMouse();
    Thread.Sleep(1000);
}

Console.WriteLine("Processo concluído!");

void MoverCursorAleatoriamente()
{
    int larguraTela = ObterLarguraTela();
    int alturaTela = ObterAlturaTela();

    int posicaoX = geradorAleatorio.Next(larguraTela);
    int posicaoY = geradorAleatorio.Next(alturaTela);

    SetCursorPos(posicaoX, posicaoY);

    Console.WriteLine($"Cursor movido para X: {posicaoX}, Y: {posicaoY}");
}

void SimularCliqueMouse()
{
    mouse_event(EVENTO_CLIQUE_ESQUERDO_PRESS, 0, 0, 0, 0);
    mouse_event(EVENTO_CLIQUE_ESQUERDO_SOLTA, 0, 0, 0, 0);

    Console.WriteLine("Clique do mouse simulado!");
}

int ObterLarguraTela()
{
    IntPtr handleContexto = GetDC(IntPtr.Zero);
    int largura = GetDeviceCaps(handleContexto, RESOLUCAO_LARGURA);
    ReleaseDC(IntPtr.Zero, handleContexto);
    return largura;
}

int ObterAlturaTela()
{
    IntPtr handleContexto = GetDC(IntPtr.Zero);
    int altura = GetDeviceCaps(handleContexto, RESOLUCAO_ALTURA);
    ReleaseDC(IntPtr.Zero, handleContexto);
    return altura;
}
