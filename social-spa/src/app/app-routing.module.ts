import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from './components/nav/nav.component';
import { FeedComponent } from './components/feed/feed.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PendingRequestsComponent } from './components/widgets/pending-requests/pending-requests.component';
import { UserFriendsComponent } from './components/widgets/user-friends/user-friends.component';
import { PrivacySettingsComponent } from './components/privacy-settings/privacy-settings.component';
import { SignupComponent } from './components/signup/signup.component';

const routes: Routes = [
  {
    path: 'home',
    component: NavComponent,
    children: [
      { path: 'user-profile', component: UserProfileComponent },
      { path: 'user-profile/:id', component: UserProfileComponent },
      { path: 'feed', component: FeedComponent },
      { path: 'pendingrequests', component: PendingRequestsComponent },
      { path: 'friends', component: UserFriendsComponent },
      { path: 'privacy-settings', component: PrivacySettingsComponent },
    ],
  },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: '', component: LoginComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
