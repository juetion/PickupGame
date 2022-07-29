
int[] initGrid = new int[] { 3,5,7 };//可自定义多少行多少数量
string[] players = new string[] { "A", "B" };//可自定义玩家数量和玩家名称
int step = 0;//记录经过了多少步
printGrid();//打印物品现有情况
startGame();//开始游戏

/**
 * 打印物品现有情况
 */
void printGrid()
{
    Console.WriteLine("物品剩余数量：");
    Console.WriteLine(string.Join(",",initGrid));
}

/**
 * 开始游戏
 */
void startGame()
{
    int playerNum = players.Length;
    string player = null;
    do
    {
        player = players[step % playerNum];//当前进行操作的玩家
        doStep(player);
    } while (!verifyResult(player)); //判断是否获胜
}

/**
 * 玩家进行操作
 */
void doStep(string player)
{
    int line = 0, num = 0; //记录玩家需要操作的行，和数量
    string input = null;
    while (input == null)
    {
        Console.WriteLine("请"+player+"玩家输入两位数，逗号分隔（第一位数表示取第几行的物品，第二位数表示取几个物品，回车确定）：");
        input = Console.ReadLine();
        if (!input.Contains(","))//如果玩家没有逗号分割输入数字
        {
            input = null;
            Console.WriteLine("输入错误：没有逗号分割！");
            continue;
        }
        string[] ins = input.Split(",");
        if (ins.Length != 2)//如果玩家输入的不是两个数字
        {
            input = null;
            Console.WriteLine("输入错误：没有输入两位数！");
            continue;
        }
        try
        {
            line = int.Parse(ins[0]);
            num = int.Parse(ins[1]);
        }
        catch (Exception e) //捕获异常，玩家输入的不是数字的情况
        {
            input = null;
            line = 0;
            num = 0;
            Console.WriteLine("输入错误：输入的内容不是数字！");
            continue;
        }

        if (line > initGrid.Length || line < 1) //判断所输入行数数据是否符合要求
        {
            input = null;
            line = 0;
            num = 0;
            Console.WriteLine("输入错误：输入的行数超过最大行数或行数不是正数！");
            continue;
        }

        int remainNum = initGrid[line - 1];
        if (remainNum < num || num < 1)//判断所输入数量数据是否符合要求
        {
            input = null;
            line = 0;
            num = 0;
            Console.WriteLine("输入错误：输入的要取物品的数量超过该行剩余最大数量或数量不是正数！");
            continue;
        }
        //当所有的数据都校验没问题了，进行实际的操作
        initGrid[line - 1] = remainNum - num;
    }
    step++;//记录操作了一步
    printGrid();//打印物品现有情况
}

/**
 * 校验结果，如果玩家获胜了，返回true
 */
Boolean verifyResult(string player)
{
    Boolean win = true;
    //循环判断数据是否都为0
    Array.ForEach<int>(initGrid, (int i) =>
    {
        if (i > 0)
        {
            win = false;
        }
    });
    
    if (win) //玩家胜利了
    {
        Console.WriteLine(player + "玩家胜利！");
        return win;
    }
    return false;
}