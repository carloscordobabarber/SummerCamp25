import { Component, Input } from '@angular/core';
import { Apartment } from '../../models/apartment';

@Component({
  selector: 'app-cards',
  standalone: false,
  templateUrl: './cards.html',
  styleUrl: './cards.css'
})
export class Cards {
  @Input() apartment!: Apartment;
}
