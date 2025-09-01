import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-date-range-filter',
    standalone: false,
    templateUrl: './date-range-filter.html',
    styleUrls: ['./date-range-filter.css']
})
export class DateRangeFilter {
    @Input() label: string = '';
    @Input() startDate: string = '';
    @Input() endDate: string = '';
    @Output() dateRangeChange = new EventEmitter<{ startDate: string, endDate: string }>();

    public get _startDate() { return this.startDate; }
    public set _startDate(val: string) {
        this.startDate = val;
        this.emitChange();
    }
    public get _endDate() { return this.endDate; }
    public set _endDate(val: string) {
        this.endDate = val;
        this.emitChange();
    }

    emitChange() {
        this.dateRangeChange.emit({ startDate: this.startDate, endDate: this.endDate });
    }
}
