import { Component } from '@angular/core';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {


  navRoutes = [{
    title: 'Nonplanar',
    route:'/'
  },
  {
    title: 'Test',
    route: '/test',
    rights: 'admin'
  },
  {
    title: 'Login',
    route: '/login'
  }
  ];


}
