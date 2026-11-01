# ConsoleUtf8

一个很小的 CounterStrikeSharp 插件，用来解决 **CS2 服务端控制台中文/Unicode 显示乱码** 的问题。

## 依赖

- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)（CSS 插件框架，**必须**）
- [Metamod:Source 2](https://www.metamodsource.net/downloads.php/?branch=master)（CSS 的前置依赖）
- 编译需要 .NET SDK（项目目标框架 `net10.0`）

## 安装

1. 先装好 Metamod:Source 2 和 CounterStrikeSharp；
2. 把编译好的 DLL 放到插件目录：

```text
game/csgo/addons/counterstrikesharp/plugins/ConsoleUtf8/
├─ ConsoleUtf8.dll
└─ ConsoleUtf8.deps.json
```

3. 重启服务器（或让 CSS 热加载插件）。加载成功后控制台会输出一行：

```text
[ConsoleUtf8] plugin loaded
```

## 编译

```bash
dotnet build -c Release
```

输出：

```text
build/Release/ConsoleUtf8.dll
build/Release/ConsoleUtf8.deps.json
```

## 说明

- 插件在 CSS 加载时才生效，所以最早期的几行引擎输出（CSS 加载之前）仍然是系统代码页；那些内容基本都是英文，不影响使用。
- 如果中文显示成**方块**（不是乱码），说明控制台字体没有中文字形：右键窗口标题 → 属性 → 字体 → 选择「新宋体」。
- 不需要时，删除 `plugins/ConsoleUtf8/` 目录并重启服务器即可。
