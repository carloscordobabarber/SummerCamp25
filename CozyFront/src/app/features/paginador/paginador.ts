import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-paginador',
  standalone: true,
  templateUrl: './paginador.html',
  styleUrl: './paginador.css'
})
export class Paginador {
  @Input() page: number = 1;
  @Input() pageSize: number = 10;
  @Input() totalItems: number = 0;
  @Input() pageSizeOptions: number[] = [5, 10, 20, 50];

  @Output() pageChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.totalItems / this.pageSize));
  }

  goToPage(p: number) {
    if (p >= 1 && p <= this.totalPages && p !== this.page) {
      this.pageChange.emit(p);
    }
  }

  changePageSize(size: number) {
    this.pageSizeChange.emit(Number(size));
  }

  onPageSizeSelect(event: Event) {
    const value = +(event.target as HTMLSelectElement).value;
    this.changePageSize(value);
  }
}
