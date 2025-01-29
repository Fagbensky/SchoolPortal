import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-not-found',
  standalone: true,
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.scss'
})
export class NotFoundComponent {
  @Input() title = '';
  @Input() body?: string = '';
  @Input() buttonText = '';
  @Output() buttonEvent = new EventEmitter();
}
