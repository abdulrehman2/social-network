import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-add-post-dialog',
  templateUrl: './add-post-dialog.component.html',
  styleUrls: ['./add-post-dialog.component.scss'],
})
export class AddPostDialogComponent implements OnInit {
  @Input() isVisible: boolean = false;
  @Output() closeDialog = new EventEmitter();
  constructor() {}

  ngOnInit(): void {}

  onDialogHide(event: any) {
    debugger;
    this.closeDialog.emit(false);
  }
}
