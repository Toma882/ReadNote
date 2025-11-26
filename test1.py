# 1. 计算旅游费用
from ast import If


# 变量声明和使用

r = int(input("输入人数: "))
f = 50* 0.88 * r
print("费用",f)


# 2. 体能成绩

# 逻辑运算符， if XXX： else： 
# ① = float， ② = 3， ③=s

s= float(input("输入体能成绩: "))

if s >= 295.5:
    print("合格")
else: 
    print("不及格")


# 3. 输入2到200之间的3 的倍数

# 循环语句， range(start, end, step): 开始，结束，步长  
# 开始是3，结束是200，步长是3
# ① = 0， ② = 3， ③=s
s = 0
for i in range(3, 200, 3):
    s = s + i
print("2到200之间的3 的倍数", s)