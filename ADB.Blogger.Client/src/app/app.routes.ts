import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { DisplayPostComponent } from './display-post/display-post.component';
import { DisplayPostListComponent } from './display-post-list/display-post-list.component';
import { TestComponent } from './test/test.component';
import { PostFormComponent } from './post-form/post-form.component';

export const APP_ROUTES: Routes = [
    {
        path:'',
        pathMatch:'full',
        redirectTo:'post'
    },
    {
      path:'post/new', 
      component:PostFormComponent
    },
    {
      path:'post/edit/:id',
      component:PostFormComponent
    },
    {
      path:'post/:id',
      component: DisplayPostComponent
    },
    {
      path:'post',
      component: DisplayPostListComponent
    },
    {
      path:'login',
      component: LoginComponent,       
    },
    {
      path:'test',
      component: TestComponent
    }

];

@NgModule({
    imports: [
      RouterModule.forRoot(
        APP_ROUTES,
        {
          enableTracing: false,
          // <-- debugging purposes only
          bindToComponentInputs:true
        },
      )
    ],
    exports: [
      RouterModule
    ]
  })
  export class AppRoutingModule { }