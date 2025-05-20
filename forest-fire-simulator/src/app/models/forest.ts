export interface Forest {
  size: number;
  cells: TreeState[][]; // tableau jagged
}

export type TreeState = 'Empty' | 'Tree' | 'Fire' | 'Ash';
