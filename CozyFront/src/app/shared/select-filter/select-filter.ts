import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-select-filter',
  standalone: false,
  templateUrl: './select-filter.html'
})
export class SelectFilterComponent {
  @Input() label: string = '';
  @Input() options: any[] = [];
  @Input() placeholder: string = '-';
  @Input() selectId: string = '';
  @Input() value: any;
  @Output() valueChange = new EventEmitter<any>();
  @Output() change = new EventEmitter<any>();

  onChange(event: Event) {
    this.valueChange.emit(this.value);
    this.change.emit(event);
  }
}
