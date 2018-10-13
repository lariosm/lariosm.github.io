# Homework 3

For this next assignment, we were tasked with translating Java code to C# (pronounced "See sharp").

## Links

* Assignment page can be found [here](http://www.wou.edu/~morses/classes/cs46x/assignments/HW3_1819.html).
* Code repository referencing this work can be found [here](https://github.com/mlarios1/CS460/tree/master/hw3).
* Clone repo link: [https://github.com/mlarios1/CS460.git](https://github.com/mlarios1/CS460.git)

## Step 1: Preparation

Given my background in Java, C# wasn't as different and intimidating as I initially thought. However, there were certain syntax and naming conventions I had to familiarize myself with. For example, names on ```public``` and ```protected``` fields and methods must be ```PascalCased```,

```C#
public string MyString
```

whereas ```private``` fields, parameters and local variables should be ```camelCased```.

Another example would be instead of inheriting a class using ```extends```, like I would with Java, I would place a colon in between the class name and the inherited class like so:

```C#
public class MyClass : InheritedClass
```

and so forth.

## Step 2: Analyzing Java Code

Having completed the previous step, I downloaded [javacode2.zip](http://www.wou.edu/~morses/classes/cs46x/assignments/javacode2.zip), extracted its contents and analyzed each Java file to see what part they played in making the program work. Next, I compiled main.java using the command line in Windows and played around with the program.

![Running Java program](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/java_main.gif)

## Step 3: Translating Java to C#

After getting a sense of what the program does (it prints a binary representation of all numbers from 1 to n), I opened up Visual Studio, created a new project (or solution, as Visual Studio calls it). For the most part, it was somewhat easy, though I constantly had to remind myself to follow C# conventions and search the internet for alternatives as I went.

## 3.1: Node.cs

For ```Node.cs```, it was a matter of copying and pasting the code (Which I didn't!) and capitalizing field names.

[![Side-by-side Node class](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/nodeclass.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/nodeclass.PNG)

## 3.2 IQueueInterface.cs

The same goes with ```IQueueInterface.cs```. The only notable changes I had to make was put an "I" in front of QueueInterface (naming convention), write ```T Pop()``` without throwing ```QueueUnderflowException``` (because it wouldn't let me) and write ```bool``` instead of ```boolean```.

[![Side-by-side IQueueInterface class](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/interfaceclass.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/interfaceclass.PNG)

## 3.3 QueueUnderflowException.cs

As I translated ```QueueUnderflow.java``` to C#, I realized right away I couldn't use the ```extends``` keyword, nor ```super()```, like I was used to in Java. Instead, I had to use the colon after the class name, followed by the class I want to inherit. The same was with methods, only I used ```base()``` in place of the inherited class name (or base class, as it's called in C#).

[![Side-by-side QueueUnderflowException class](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/queueunderflowclass.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/queueunderflowclass.PNG)

## 3.4 LinkedQueue.cs

Similar to the other three classes I translated, keywords and casing of certain variables were changed. However, instead of throwing a ```NullPointerException``` as I was used to in Java (because it doesn't exist in C#), I found ```NullReferenceException``` on Google and realized it was similar, if not exactly alike, to ```NullPointerException```. On a separate note, when I tried assigning ```null``` to ```tmp``` like so

```C#
T tmp = null;
```

[![Side-by-side LinkedQueue class](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/linkedqueueclass.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/linkedqueueclass.PNG)

IntelliSense would tell me it would not compile because of the possibility that ```T``` (a generic type) could be a non-nullable value type. It suggested I use ```default(T)``` instead, which I did.

## 3.5 Program.cs

After assembling the previous four classes, I would piece them altogether in one class. As I wrote ```Program.cs```, I made sure to keep in line with the structure and names of variables in ```Main.java```. (Though I did change some names for clarity.) One of the few notable changes I made in this class was that because ```NumberFormatException``` in my ```try``` block did not exist in C#, I had to replace it with ```FormatException``` which seems to match the description very well. The other change was the structure for the "for each" loop in the ```Main``` method.

```C#
foreach(string s in output)
{
  //Code...
}
```

[![Side-by-side Program class](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/programclass.PNG)](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/programclass.PNG)

## Step 4: Output

Having completed the previous step with no reported syntax or compile errors within Visual Studio. I fire up the command line, compile the classes, which in turn gives me ```Program.exe``` and run the same commands I did back in step 2.

![Running Program.exe](https://mlarios1.github.io/mlarios1.github.io/CS460/HW3/cs_program.gif)

Given the results, I'd say this assignment was a success!

## Final thoughts

Given this was my first hands-on experience working with C#, I have to say, the learning process wasn't as bad as I initally thought. Sure, it was rough because we had less than a week to learn the language, but I think with enough time and practice, I might come to embrace C#, maybe even use it as my primary language given its widespread capabilities over Java.
