import { Component, OnInit } from '@angular/core';
import { ForestService } from '../../services/forest.service';
import { CommonModule } from '@angular/common';
import { Forest, TreeState } from '../../models/forest';

@Component({
  selector: 'app-forest',
  templateUrl: './forest.component.html',
  styleUrls: ['./forest.component.scss'],
  imports: [CommonModule],
})
export class ForestComponent implements OnInit {
  forest: Forest | null = null;
  size = 20;

  constructor(private forestService: ForestService) {}

  ngOnInit() {
    this.initForest();
  }

  initForest() {
    this.forestService.initForest(this.size).subscribe((data) => {
      this.forest = data;
    });
  }

  nextStep() {
    if (!this.forest) return;
    this.forestService.step(this.forest).subscribe((data) => {
      this.forest = data;
    });
  }

  saveCurrentForest() {
    if (this.forest == null) return;
    this.forestService.save(this.forest).subscribe((data) => {
      //this.forest = data;
    });
  }

  restoreForest() {
    this.forestService.restore('.').subscribe((data) => {
      this.forest = data;
    });
  }

  getEmoji(cell: TreeState): string {
    switch (cell) {
      case 0:
        return '🟫';
      case 1:
        return '🌲';
      case 2:
        return '🔥';
      case 3:
        return '⬛';
      default:
        return '?';
    }
  }
}
