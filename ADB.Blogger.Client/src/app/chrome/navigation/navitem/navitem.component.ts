import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-navitem',
  templateUrl: './navitem.component.html',
  styleUrls: ['./navitem.component.css']
})
export class NavitemComponent {
  @Input() itemTitle: string | undefined;
  @Input() itemRoute: string | undefined; // 
  @Input() itemRights: string | undefined; //

}
