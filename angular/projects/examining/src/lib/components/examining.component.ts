import { Component, OnInit } from '@angular/core';
import { ExaminingService } from '../services/examining.service';

@Component({
  selector: 'lib-examining',
  template: ` <p>examining works!</p> `,
  styles: [],
})
export class ExaminingComponent implements OnInit {
  constructor(private service: ExaminingService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
