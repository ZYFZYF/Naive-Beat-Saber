# Naive-Beat-Saber

《虚拟现实技术》课程大作业

## TODO

- 方块
	- 显示
		- 纹理（只贴正面、只有正面有方向）
	- 出现
		- 一次加载完or每次加载一定数量的
		- 这块代码运行在哪里？
		- 编码方式
			- 至少有颜色（表示左右手）、X轴坐标（可以是-2到+2，表示到中心的offset）、Y轴坐标同理、旋转角度（每45度算一个共8个，0-7表示正上方逆时针旋转多少个45度，再加上一个-1表示任意角度都可？）
			- 存储成什么文件？用什么读取？(xml or json？或者直接csv？)
				- xml 可读，但是量大
				- json也可读，还可以存储其他数据，但是也是不容易批量修改
				- csv方便批量修改（用excel or numbers）
		- 爆炸
			- 简化：可以从中间劈开（毁掉原来的物体，新建两个带重力的各一半，然后施加一个爆炸的瞬时力或者沿切割方向切线的顺势力 
- 光剑
	- 模型
	  - [Unity Laser Sword - Volumetric Laser Sword in Unity. by Jeff Johnson (jjxtra)](https://github.com/jjxtra/UnityLaserSword)
	- 碰撞检测
		- 用自带的，然后在回调里去判断切割方向对不对	
- 分数统计
	- 用内置的PlayerPrefs类（自带的一个key-value的持久化存储工具）
- 网络通讯
	- 用unity的什么模块？
	- 交互json格式的数据
- 交互方式
	- 拳击模式以及新型消方块的手段还要做嘛（这样可能刚开始还需要一个选择模式的界面，可以用手势识别来控制  上划  下划）
