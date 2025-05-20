export interface Forest {
  size: number;
  cells: TreeState[][]; // tableau jagged
}

// 0 | 1 | 2 | 3 = 'Empty' | 'Tree' | 'Fire' | 'Ash';
export type TreeState = 0 | 1 | 2 | 3;
