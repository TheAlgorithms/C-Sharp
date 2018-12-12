using System;

namespace binary_insertion_sort {
		class Program {
		private const int ARRAY_SIZE = 20;

		static void Main( string[ ] args ) {
			Random rnd = new Random( DateTime.Now.Millisecond );

			int[ ] data = new int[ ARRAY_SIZE ];

			for ( int i = 0; i < ARRAY_SIZE; ++i ) {
				data [ i ] = rnd.Next( ARRAY_SIZE );
			}

			Console.Write( "Array before sorting: " );
			PrintArray( data );

			BinaryInsertionSort( data );

			Console.Write( "Array after sorting: " );
			PrintArray( data );
		}

		/// <summary>An implementation of the Binary Insertion Sort.</summary>
		/// <param name="data">An array of integer values to be sorted.</param>
		private static void BinaryInsertionSort( int[ ] data ) {
			for ( int targetIndex = 1; targetIndex < data.Length; ++targetIndex ) {
				int target = data [ targetIndex ],
					moveIndex = targetIndex - 1,
					targetInsertLocation = BinarySearch( data, 0, moveIndex, target );

				while ( moveIndex >= targetInsertLocation ) {
					data [ moveIndex + 1 ] = data [ moveIndex ];
					--moveIndex;
				}

				data [ targetInsertLocation ] = target;
			}
		}

		/// <summary>Implementation of Binary Search using stack recursion.</summary>
		/// <param name="data">An array of integer values -in ascending sorted order between the index values left and right- to search through.</param>
		/// <param name="left">The value of the starting left hand index.</param>
		/// <param name="right">The value of the starting right hand index.</param>
		/// <param name="target">The value to find in the provided data.</param>
		/// <returns>The index an instance of target if it exists within data; else -1.</returns>
		private static int BinarySearch( int[ ] data, int left, int right, int target ) {
			while ( right >= left ) {
				int middle = ( left + right ) / 2;

				if ( data [ middle ] == target ) {
					return middle + 1;
				}

				if ( target < data [ middle ] ) {
					right = middle - 1;
				} else {
					left = middle + 1;
				}
			}

			return target > data [ left ] ? left + 1 : left;
		}

		/// <summary>Prints an array of integer values.</summary>
		/// <param name="data">An array of integer values to be printed.</param>
		private static void PrintArray( int[ ] data ) {
			foreach ( var datum in data ) {
				Console.Write( $"{datum.ToString()} " );
			}

			Console.WriteLine();
		}
	}
}