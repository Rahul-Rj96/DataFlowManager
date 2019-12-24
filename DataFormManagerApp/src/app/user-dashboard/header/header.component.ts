import { Component, OnInit ,Input} from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Input() name: string;
  @Input() button1: string;
  @Input() button2: string;
  constructor() { }

  ngOnInit() {
  }


}
