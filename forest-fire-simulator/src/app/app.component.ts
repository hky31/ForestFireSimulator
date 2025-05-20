import { Component } from '@angular/core';
import { ForestComponent } from './components/forest/forest.component';

@Component({
  selector: 'app-root',
  imports: [ForestComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'forest-fire-simulator';
}
