import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Hero } from '../shared/models/hero.model';
import { Observable } from 'rxjs';
import { User } from '../shared/models/user.model';
import { Injectable } from '@angular/core';

@Injectable()
export class HeroService {
    baseUrl = environment.apiUrl;
  
    constructor(private http: HttpClient) {}

    getHeroes(): Observable<Hero[]> {

        return this.http.get<Hero[]>(this.baseUrl + 'heroes/');
    }

    trainHero(hero : Hero): Observable<Hero> {

        return this.http.post<Hero>(this.baseUrl + `heroes/train/${hero.id}`,null);
    }
}