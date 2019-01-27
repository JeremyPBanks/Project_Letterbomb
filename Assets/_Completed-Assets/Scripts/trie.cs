using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class EnglishDictionary
{
	private TreeNode root;

	public EnglishDictionary(string fileName)
	{
		root = new TreeNode(false,0);
		BuildTrie(fileName);
	}
	
	protected void BuildTrie(string fileName)
	{
		int lLen = 0;
		int asciiVal = 0;
		TreeNode ptr;
		var lines = File.ReadLines(fileName);
		foreach (var line in lines)
		{
			lLen = line.Length;
			ptr = root;
			for(int i = 0; i < lLen; i++)
			{
				asciiVal = ((int) line[i]) - 65;
				if (ptr.Alpha[asciiVal] == null)
				{
					if ((i+1) == lLen)
					{
						ptr.Alpha[asciiVal] = new TreeNode(true,i+1);
						break;
					}
					ptr.Alpha[asciiVal] = new TreeNode(false,i+1);
					ptr = ptr.Alpha[asciiVal];
				}
				else
				{
					ptr = ptr.Alpha[asciiVal];
				}
			}
		}
	}
	
	public bool SearchTrie(string target)
	{
		//recursiveTraversal(root);
		int asciiVal = 0;
		int tLen = target.Length;
		TreeNode ptr = root;
		for(int i = 0; i < tLen; i++)
		{
			asciiVal = ((int) target[i]) - 65;
			if (ptr.Alpha[asciiVal] == null)
			{
				return false;
			}
			else
			{
				ptr = ptr.Alpha[asciiVal];
			}
		}
		return ptr.isWord;
	}
	
	public void recursiveTraversal(TreeNode node)
	{
		for(int j = 0; j < 26; j++)
		{
			if(node.Alpha[j] != null)
			{
				recursiveTraversal(node.Alpha[j]);
			}
		}
		return;
	}
}

public class TreeNode
{
	public TreeNode[] Alpha;
	public bool isWord;
	public readonly int length;

	public TreeNode(bool boolean, int length)
	{
		this.isWord = boolean;
		this.length = length;
		this.Alpha = new TreeNode[26];
	}
}

namespace MyProgram
{
	class MyProgramClass
	{
		static void Main()
		{
			Console.WriteLine("The Raven That Refused to Sing");
			EnglishDictionary ed = new EnglishDictionary("fullDict.txt");
			Console.WriteLine(ed.SearchTrie("IN").ToString());
			Console.WriteLine(ed.SearchTrie("COHEED").ToString());
			Console.WriteLine(ed.SearchTrie("BETWEEN").ToString());
			Console.WriteLine(ed.SearchTrie("LAY").ToString());
			Console.WriteLine(ed.SearchTrie("EXCOGITATE").ToString());
			Console.WriteLine(ed.SearchTrie("GONE").ToString());
		}
	}
}
