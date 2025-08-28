import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-range-slider-filter',
  templateUrl: './range-slider-filter.html',
  styleUrls: ['./slider-filter.css'],
  standalone: false
})
export class RangeSliderFilter {
  activeThumb: 'min' | 'max' = 'min';
  @Input() min = 0;
  @Input() max = 100;
  @Input() step = 1;
  @Input() minValue = 0;
  @Input() maxValue = 100;
  @Input() label = '';
  @Output() minValueChange = new EventEmitter<number>();
  @Output() maxValueChange = new EventEmitter<number>();
  @Output() rangeChange = new EventEmitter<{min: number, max: number}>();

  onMinInput(event: Event) {
    const val = +(event.target as HTMLInputElement).value;
    this.minValue = val;
    if (this.minValue > this.maxValue) {
      this.maxValue = this.minValue;
    }
    this.minValueChange.emit(this.minValue);
    this.rangeChange.emit({min: this.minValue, max: this.maxValue});
  }

  onMaxInput(event: Event) {
    const val = +(event.target as HTMLInputElement).value;
    this.maxValue = val;
    if (this.maxValue < this.minValue) {
      this.minValue = this.maxValue;
    }
    this.maxValueChange.emit(this.maxValue);
    this.rangeChange.emit({min: this.minValue, max: this.maxValue});
  }
}
