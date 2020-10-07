import { Helpers } from './helpers/helpers';
import { RouterModule } from '@angular/router';
import { AppComponent } from './layout/app/app.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FooterComponent } from './layout/footer/footer.component';
import { HeadComponent } from './layout/head/head.component';
import { LoginComponent } from './components/login/login.component';
import { FeedComponent } from './components/feed/feed.component';
import { ProfileComponent } from './components/profile/profile.component';
import { PhotocardComponent } from './shared/photocard/photocard.component';
import { SearchComponent } from './shared/search/search.component';
import { ImageUploadComponent } from './components/image-upload/image-upload.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeadComponent,
    LoginComponent,
    FeedComponent,
    ProfileComponent,
    PhotocardComponent,
    SearchComponent,
    ImageUploadComponent,
  ],
  imports: [
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserModule 
  ],
  providers: [Helpers],
  bootstrap: [AppComponent]
})
export class AppModule { }
