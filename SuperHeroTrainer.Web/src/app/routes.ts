import {Routes} from '@angular/router';
 import { HomeComponent } from './components/home/home.component';
// import { MemberListComponent } from './members/member-list/member-list.component';
// import { MessagesComponent } from './messages/messages.component';
// import { ListsComponent } from './lists/lists.component';
 import { AuthGuard } from './_guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { EditTrainerComponent } from './components/admin/edit-trainer/edit-trainer.component';
import { RegistrationTrainerComponent } from './components/admin/registration-trainer/registration-trainer.component';
import { WelcomeScreenComponent } from './components/welcome-screen/welcome-screen.component';
// import { MemberDetailComponent } from './members/member-detail/member-detail.component';
// import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
// import { MemberListResolver } from './_resolvers/member-list.resolver';
// import { MemberEditComponent } from './members/member-edit/member-edit.component';
// import { MemberEditResolver } from './_resolvers/member-edit.resolver';
// import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
// import { ListsResolver } from './_resolvers/lists.resolver';
// import { MessagesResolver } from './_resolvers/messages.resolver';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent,  canActivate: [AuthGuard],
      children: [    
        { path: '', pathMatch: 'full', redirectTo: 'welcome'},
        { path: 'welcome', component: WelcomeScreenComponent },
        { path: 'admin', component: AdminComponent , canActivate: [AuthGuard],
            children: [
              { path: '', pathMatch: 'full', redirectTo: 'edit'},
              { path: 'edit', component: EditTrainerComponent  },
              { path: 'registration', component: RegistrationTrainerComponent  },
            ] },
          ]},
    { path: 'login', component: LoginComponent },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];