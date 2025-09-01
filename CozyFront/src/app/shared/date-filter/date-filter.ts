import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-date-filter',
  standalone: false,
  templateUrl: './date-filter.html',
  styleUrls: ['./date-filter.css']
})
export class DateFilter {
  @Input() label: string = '';
  @Input() date: string = '';
  @Output() dateChange = new EventEmitter<string>();

  onDateChange(event: Event) {
    this.date = (event.target as HTMLInputElement).value;
    this.dateChange.emit(this.date);
  }
}
