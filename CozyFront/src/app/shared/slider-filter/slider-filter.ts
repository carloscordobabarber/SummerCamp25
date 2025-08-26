import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-slider-filter',
  templateUrl: './slider-filter.html',
  standalone: false,
  styleUrls: ['./slider-filter.css']
})
export class SliderFilter {
  @Input() min = 0;
  @Input() max = 100;
  @Input() step = 1;
  @Input() value = 0;
  @Input() label = '';
  @Output() valueChange = new EventEmitter<number>();

  onInput(event: Event) {
    const val = +(event.target as HTMLInputElement).value;
    this.value = val;
    this.valueChange.emit(val);
  }
}
