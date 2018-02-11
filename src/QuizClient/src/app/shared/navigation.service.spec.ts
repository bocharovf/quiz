import { NavigationService } from './navigation.service';
import { Router } from '@angular/router/src/router';

describe('NavigationService', () => {
  let router: any;
  let service: NavigationService;

  beforeEach(() => {
    router = jasmine.createSpyObj(['navigate']);
    service = new NavigationService(router);
  });

  describe('goToQuiz', () => {
    it('should navigate to quiz by id', () => {
      service.goToQuiz(1);
      expect(router.navigate).toHaveBeenCalledWith(['quizzes/1']);
    });
  });

  describe('goToQuizScores', () => {
    it('should navigate to quiz scores by id', () => {
      service.goToQuizScores(2);
      expect(router.navigate).toHaveBeenCalledWith(['quizzes/2/scores']);
    });
  });
});
