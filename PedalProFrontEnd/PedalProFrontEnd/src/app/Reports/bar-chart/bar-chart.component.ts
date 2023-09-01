import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-bar-chart',
  template: `
    <div style="display: block;">
      <canvas baseChart
              [datasets]="barChartData"
              [labels]="barChartLabels"
              [options]="barChartOptions"
              [legend]="barChartLegend"
              type="bar">
      </canvas>
    </div>
  `
})
export class BarChartComponent {
  @Input() barChartData: any[] = [];
  @Input() barChartLabels: string[] = [];
  @Input() barChartOptions: any;
  @Input() barChartLegend = true;
  
}
