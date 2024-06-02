import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NavigationComponent } from './chrome/navigation/navigation.component';
import { UsermenuComponent } from './chrome/usermenu/usermenu.component';
import { LoginComponent } from './login/login.component'
import { AppRoutingModule } from './app.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { NavitemComponent } from './chrome/navigation/navitem/navitem.component';
import { DisplayPostComponent } from './display-post/display-post.component';
import { DisplayPostListComponent } from './display-post-list/display-post-list.component';
import { TestComponent } from './test/test.component';
import { PostFormComponent } from './post-form/post-form.component';

import { EditorModule } from '@tinymce/tinymce-angular';
import { ModalComponent } from './chrome/modal/modal.component';
import { ButtonComponent } from './chrome/button/button.component';
import { HighlightCardComponent } from './cards/highlight-card/highlight-card.component';
import { HeadingCardComponent } from './cards/heading-card/heading-card.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    UsermenuComponent,
    LoginComponent,
    NavitemComponent,
    DisplayPostComponent,
    DisplayPostListComponent,
    TestComponent,
    PostFormComponent,
    ModalComponent,
    ButtonComponent,
    HighlightCardComponent,
    HeadingCardComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule, ReactiveFormsModule, EditorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
