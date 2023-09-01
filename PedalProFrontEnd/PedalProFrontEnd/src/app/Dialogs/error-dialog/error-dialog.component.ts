import { Component ,Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-error-dialog',
  template: `
    <div class="error-dialog">
      <h2>Error</h2>
      <p>{{ data.message }}</p>
      <button mat-button [mat-dialog-close]="true">Close</button>
    </div>
  `,
  styles: [
    `
      .error-dialog {
        text-align: center;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        background-color: white;
        border: 1px solid black
      }

      h2 {
        color: #f44336;
      }

      p {
        margin-bottom: 16px;
      }

      button {
        background-color: #f44336;
        color: white;
        border:1px solid black
      }
    `,
  ],
})


export class ErrorDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string }) {}
}
