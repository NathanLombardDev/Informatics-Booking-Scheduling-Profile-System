import { Component, Input } from '@angular/core';
import { ChartDataset, ChartOptions,ChartTypeRegistry } from 'chart.js';

@Component({
  selector: 'app-pie-chart',
  template: `
    <div style="display: block;">
      <canvas baseChart
              [datasets]="pieChartData"
              [labels]="pieChartLabels"
              [options]="pieChartOptions"
              [legend]="pieChartLegend"
              [type]="pieChartType">
      </canvas>
    </div>
  `
})
export class PieChartComponent {
  @Input() pieChartData: ChartDataset[] = [];
  @Input() pieChartLabels: string[] = [];
  @Input() pieChartOptions: ChartOptions = {};
  @Input() pieChartLegend = true;
  @Input() pieChartType: keyof ChartTypeRegistry = 'pie';
}
