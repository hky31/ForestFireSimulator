import { Component, OnInit } from "@angular/core";
import { ForestService } from "../../services/forest.service";
import { CommonModule } from "@angular/common";
import { Forest, TreeState } from "../../models/forest";
import { MatSliderModule } from "@angular/material/slider";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { FormsModule } from "@angular/forms";

@Component({
	selector: "app-forest",
	standalone: true,
	imports: [
		CommonModule,
		MatSliderModule,
		MatButtonModule,
		MatCardModule,
		FormsModule,
	],
	templateUrl: "./forest.component.html",
	styleUrls: ["./forest.component.scss"],
})
export class ForestComponent implements OnInit {
	forest: Forest | null = null;
	size = 30;
	isRunning = false;
	stepCount = 0;
	intervalValue = 1000;
	private intervalId: any = null;

	constructor(private forestService: ForestService) {}

	ngOnInit() {
		this.initForest();
	}

	setForestSize(value: string) {
		this.size = Number(value);
		this.initForest();
	}

	initForest() {
		this.forestService.initForest(this.size).subscribe((data) => {
			this.forest = data;
		});
		this.stepCount = 0;
	}

	nextStep() {
		if (!this.forest) return;
		this.forestService.step(this.forest).subscribe((data) => {
			this.forest = data;
		});
		this.stepCount++;
	}

	toogleSimulation() {
		if (this.isRunning) {
			// Pause
			clearInterval(this.intervalId);
			this.intervalId = null;
			this.isRunning = false;
		} else {
			// Reprise
			this.intervalId = setInterval(() => {
				this.nextStep(); // avance le feu
			}, this.intervalValue); // intervalle en ms (ajuste à ta convenance)

			this.isRunning = true;
		}
	}

	saveCurrentForest() {
		if (this.forest == null) return;
		this.forestService.save(this.forest).subscribe((data) => {
			//this.forest = data;
		});
	}

	restoreForest() {
		this.forestService.restore(".").subscribe((data) => {
			this.forest = data;
		});
	}

	getEmoji(cell: TreeState): string {
		switch (cell) {
			case 0:
				return "🟫";
			case 1:
				return "🌲";
			case 2:
				return "🔥";
			case 3:
				return "⬛";
			default:
				return "?";
		}
	}
}
