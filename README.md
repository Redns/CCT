# CCT

![version: v1.0.3 (shields.io)](https://img.shields.io/badge/version-v1.0.3-green) ![version: v1.0.0 (shields.io)](https://img.shields.io/badge/.net-v6.0-orange) ![version: v1.0.0 (shields.io)](https://img.shields.io/badge/License-MIT-blue)

## Background

目前，通信领域的计算库较少且难以兼顾轻量、易用、跨平台等特性，给该领域的从业人员带来不少困扰。[CCT](https://github.com/Redns/CCT) 即是一款同时兼顾上述特性的 .NET 库，可完成信源编码/译码、信道编码/译码、数字调制/解调、均衡等多种运算。

## Install

使用 [Nuget](https://www.nuget.org/packages/CCT/1.0.1) 或其他包管理器直接搜索安装即可

![image-20220610183901455](http://imagebed.krins.cloud/api/image/X680NBZ2.png)

## Usage

[CCT](https://github.com/Redns/CCT) 将整个通信流程分为若干个模块，具体结构如下

![image-20220612153638177](http://imagebed.krins.cloud/api/image/LHL4L8FR.png)

各个模块的计算方法均位于对应的命名空间下

|           模 块            |   命名空间   |
| :------------------------: | :----------: |
|     信源（编码/译码）      | CCT.ISource  |
| 信道（编码/译码/数学模型） | CCT.IChannel |
|         调制/解调          |  CCT.Modem   |

该仓库也包含了大量示例程序

|             简 介              |                            链 接                             |
| :----------------------------: | :----------------------------------------------------------: |
| 信源编码（霍夫曼、费诺、香农） | [Go](https://github.com/Redns/CCT/tree/master/test/SourceEncoding) |
|        信源编码 & 译码         | [Go](https://github.com/Redns/CCT/tree/master/test/SourceEncodingAndDecoding) |

## Q & A

