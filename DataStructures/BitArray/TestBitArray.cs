///**
// * Author: Christian Bender
// * Class: TestBitArray
// * 
// * This class contains some tests for the class BitArray.
// * 
// * */


//using NUnit.Framework;
//using System;

//namespace DataStructures.BitArray
//{
//	[TestFixture ()]
//	public class TestBitArray
//	{
//		/*
//		 * Test for methods Compile(string) and ToString()
//		 * */
//		[Test ()]
//		[ExpectedException ("System.Exception")]
//		public void TestCompileToString ()
//		{
//			BitArray testObj = new BitArray (5);

//			testObj.Compile ("00100");
//			Assert.AreEqual (testObj.ToString (), "00100");

//			testObj.Compile ("01101");
//			Assert.AreEqual (testObj.ToString (), "01101");

//			testObj.Compile ("100"); // appropriate scaling
//			Assert.AreEqual (testObj.ToString (), "00100");

//			// throws exception
//			testObj.Compile ("klgml");
//			testObj.Compile ("010111111");
//		}


//		/*
//		 * Test constructor (sequence : string)  and constructor (bits : bool[])
//		 * */
//		[Test ()]
//		[ExpectedException ("System.Exception")]
//		public void TestConstructor ()
//		{
//			BitArray testObj1 = new BitArray ("00100");
//			BitArray testObj2 = new BitArray ("000120"); // throws exception
//			BitArray testObj3 = new BitArray (new bool[3] { true, false, true });

//			Assert.AreEqual (testObj1.ToInt64 (), 4);
//			Assert.AreEqual (testObj1.ToInt64 (), 5);



//		}

//		/*
//		 * Test for method Compile(int) 
//		 * */
//		[Test ()]
//		[ExpectedException ("System.Exception")]
//		public void TestCompileInteger ()
//		{
//			BitArray testObj = new BitArray (5);

//			testObj.Compile (17);
//			Assert.AreEqual (testObj.ToString (), "10001");

//			testObj.Compile (25);
//			Assert.AreEqual (testObj.ToString (), "11001");

//			testObj.Compile (4); // appropriate scaling
//			Assert.AreEqual (testObj.ToString (), "00100");

//			testObj.Compile (-8); // throws exception
//		}


//		/*
//		 * Test for operator & (bitwise and)
//		 * */
//		[Test ()]
//		public void TestOperatorAND ()
//		{
//			BitArray testObj1 = new BitArray (5);
//			BitArray testObj2 = new BitArray (5);
//			BitArray result = null;

//			testObj1.Compile (17);
//			testObj2.Compile (17);

//			result = testObj1 & testObj2;
//			Assert.AreEqual (result.ToString (), "10001");

//			testObj1.Compile (25);
//			testObj2.Compile (31);

//			result = testObj1 & testObj2;
//			Assert.AreEqual (result.ToString (), "11001");


//		}

//		/*
//		 * Test for operator | (bitwise or)
//		 * */
//		[Test ()]
//		public void TestOperatorOR ()
//		{
//			BitArray testObj1 = new BitArray (5);
//			BitArray testObj2 = new BitArray (5);
//			BitArray testObj3 = new BitArray (3);

//			BitArray result = null;

//			testObj1.Compile (25);
//			testObj2.Compile (30);

//			result = testObj1 | testObj2;
//			Assert.AreEqual (result.ToString (), "11111");


//			testObj2.Compile (16);
//			testObj3.Compile (4);

//			result = testObj2 | testObj3;

//			Assert.AreEqual (result.ToString (), "10100");

//		}


//		/*
//		 * Test for operator ~ (bitwise not)
//		 * */
//		[Test ()]
//		public void TestOperatorNOT ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (16);

//			testObj = ~testObj;
//			Assert.AreEqual (testObj.ToString (), "01111");

//		}

//		/*
//		 * Test for operator << (bitwise shift left)
//		 * */
//		[Test ()]
//		public void TestOperatorShiftLeft ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (16);

//			testObj = testObj << 3;
//			Assert.AreEqual (testObj.ToString (), "10000000");

//		}

//		/*
//		 * Test for operator << (bitwise shift right)
//		 * */
//		[Test ()]
//		public void TestOperatorShiftRight ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (24);

//			testObj = testObj >> 2;
//			Assert.AreEqual (testObj.ToString (), "110");

//		}

//		/*
//		 * Test for operator ^ (bitwise XOR)
//		 * */
//		[Test ()]
//		public void TestOperatorXOR ()
//		{
//			BitArray testObj1 = new BitArray (5);
//			BitArray testObj2 = new BitArray (5);


//			BitArray result = null;

//			testObj1.Compile (25);
//			testObj2.Compile (30);

//			result = testObj1 ^ testObj2;

//			Assert.AreEqual (result.ToInt32 (), 7);

//		}
			

//		/*
//		 * Test for indexer
//		 * */
//		[Test ()]
//		public void TestIndexer ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (24);


//			Assert.IsTrue (testObj [0]);
//			Assert.IsTrue (testObj [1]);

//			Assert.IsFalse (testObj [3]);

//		}


//		/*
//		 * Test for method NumberOfOneBits
//		 * */
//		[Test ()]
//		public void TestNumberOfOneBits ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (19);

//			Assert.AreEqual (testObj.NumberOfOneBits (), 3);

//		}

//		/*
//		 * Test for method NumberOfZeroBits
//		 * */
//		[Test ()]
//		public void TestNumberOfZeroBits ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (26);

//			Assert.AreEqual (testObj.NumberOfZeroBits (), 2);

//		}


//		/*
//		 * Test for the methods EvenParity() and OddParity()
//		 * */
//		[Test ()]
//		public void TestParity ()
//		{
//			BitArray testObj = new BitArray (5);
//			testObj.Compile (26);

//			Assert.IsFalse (testObj.EvenParity ());
//			Assert.IsTrue (testObj.OddParity ());

//		}

//		/*
//		 * Test for the method ToInt64()
//		 * */
//		[Test ()]
//		[ExpectedException ("System.Exception")]
//		public void TestToInt64 ()
//		{
//			BitArray testObj = new BitArray (6);
//			testObj.Compile (33);

//			Assert.AreEqual (testObj.ToInt64 (), 33);

//			BitArray testObj2 = new BitArray (70);
//			testObj2.Compile (128);

//			Assert.AreEqual (testObj2.ToInt64 (), 128); // throws a exception

//		}


//		/*
//		 * Test for the compare operations == and !=
//		 * */
//		[Test ()]
//		[ExpectedException ("System.Exception")]
//		public void TestCompare ()
//		{
//			BitArray testObj1 = new BitArray ("110");
//			BitArray testObj2 = new BitArray ("110");
//			BitArray testObj3 = new BitArray ("100");
//			BitArray testObj4 = new BitArray ("10101");

//			Assert.IsTrue (testObj1 == testObj2);
//			Assert.IsTrue (testObj1 != testObj3);
//			Assert.IsTrue (testObj1 == testObj4);
//		}


//		/*
//		 * Test for method ResetField
//		 * */
//		[Test ()]
//		public void TestResetField ()
//		{
//			BitArray testObj = new BitArray ("110");
//			testObj.ResetField ();
//			Assert.AreEqual (testObj.ToInt64 (), 0);
//		}

//		/*
//		 * Test for method SetAll
//		 * */
//		[Test ()]
//		public void TestSetAll ()
//		{
//			BitArray testObj = new BitArray ("101001");
//			testObj.SetAll (true);
//			Assert.AreEqual (testObj.ToInt64 (), 63);
//		}

//		/*
//		 * Test for method CompareTo
//		 * */
//		[Test ()]
//		public void TestCompareTo ()
//		{
//			BitArray testObj1 = new BitArray ("110");
//			BitArray testObj2 = new BitArray ("110");
//			BitArray testObj3 = new BitArray ("100");

//			Assert.AreEqual (testObj1.CompareTo (testObj3), 1);
//			Assert.AreEqual (testObj3.CompareTo (testObj1), -1);
//			Assert.AreEqual (testObj1.CompareTo (testObj2), 0);
//		}

//		/*
//		 * Test for method Clone and method Equals
//		 * */
//		[Test ()]
//		public void TestCloneEquals ()
//		{
//			BitArray testObj1 = new BitArray ("110");
//			BitArray testObj2 = (BitArray)testObj1.Clone ();

//			Assert.IsTrue (testObj1.Equals (testObj2));


//		}


//		/*
//		 * Test for interface implementation IEnumerator and IEnumerable
//		 * */
//		[Test ()]
//		public void TestMoveNextCurrent ()
//		{
//			BitArray testObj1 = new BitArray ("1111010");

//			int counterOnes = 0;
//			int counterZeros = 0;

//			foreach (bool bit in testObj1) 
//			{
//				if (bit) 
//				{
//					counterOnes++;
//				} 
//				else 
//				{
//					counterZeros++;
//				}
//			}

//			Assert.AreEqual (counterOnes, 5);
//			Assert.AreEqual (counterZeros, 2);
//		}
//	}
//}

