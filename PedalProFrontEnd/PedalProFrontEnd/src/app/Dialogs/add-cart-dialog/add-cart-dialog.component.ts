import { Component,Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-cart-dialog',
  template: `
    <h2 mat-dialog-title>{{ data.title }}</h2>
    <div mat-dialog-content>{{ data.message }}</div>
    <!-- Add more content here as needed -->
    <div mat-dialog-actions>
      <button mat-button (click)="onCancelClick()">Cancel</button>
      <button mat-button color="primary" (click)="onConfirmClick()">Confirm</button>
    </div>
  `,
})
export class AddCartDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<AddCartDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MyDialogData
  ) {}

  onCancelClick(): void {
    this.dialogRef.close(false);
  }

  onConfirmClick(): void {
    this.dialogRef.close(true);
  }
}

export interface MyDialogData {
  title: string;
  message: string;
  // Add more properties as needed for your dialog

}
