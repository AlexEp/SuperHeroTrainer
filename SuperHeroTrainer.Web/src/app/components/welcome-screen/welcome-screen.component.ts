import { Component, OnInit } from '@angular/core';

import { HeroService } from 'src/app/_services/heroes.service';

import { AlertService } from 'src/app/shared/services/alert.service';
import { Hero } from 'src/app/shared/models/hero.model';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-welcome-screen',
  templateUrl: './welcome-screen.component.html',
  styleUrls: ['./welcome-screen.component.scss']
})
export class WelcomeScreenComponent implements OnInit {
  heroes: Hero[];

  constructor(
    private authService: AuthService, 
    private heroService: HeroService,
    private alert: AlertService) { }

  ngOnInit(): void {
    this.heroService.getHeroes().subscribe(replay => {
        this.heroes = replay;
    })
  }

  getUserName() {
    return this.authService.getClames()?.UserName;
  }

  trainHero(hero:Hero){
    this.heroService.trainHero(hero).subscribe(replay => {
      var heroIndex = this.heroes.findIndex( h=> h.id == replay.id) ;
      this.heroes[heroIndex] = replay;
  },err => {
    if(err?.status == 400 && err?.error) {
      this.alert.error(err.error);
    }
    else
      console.log(err);
  })
  }

}
