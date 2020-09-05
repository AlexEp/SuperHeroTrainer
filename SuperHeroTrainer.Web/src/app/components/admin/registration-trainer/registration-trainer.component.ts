import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AlertService } from 'src/app/shared/services/alert.service';

import { UserRegister } from 'src/app/shared/models/userRegister.model';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-registration-trainer',
  templateUrl: './registration-trainer.component.html',
  styleUrls: ['./registration-trainer.component.scss']
})
export class RegistrationTrainerComponent implements OnInit {
  registerForm: FormGroup;
  
  constructor(private fb: FormBuilder,private alertService : AlertService, private authService : AuthService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required,Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(250),
        Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

    passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }


  register() {
    if (this.registerForm.valid) {

      let userRegister = new UserRegister();
      userRegister.username = this.registerForm.value.username;
      userRegister.email = this.registerForm.value.email;
      userRegister.password = this.registerForm.value.password;
      userRegister.confirmPassword = this.registerForm.value.confirmPassword;

      this.authService.register(userRegister).subscribe(() => {
            this.alertService.success('Registration successful');
       },err => {
        if(err?.status == 400 && err?.error) {
          this.alertService.error(err.error);
        }
        else
          console.log(err);
      })
    }
  }

}
