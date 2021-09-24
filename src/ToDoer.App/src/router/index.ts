import { createRouter, createWebHashHistory } from 'vue-router';
import routes from 'virtual:generated-pages';
import authenticationService from '../services/authentication/AuthenticationService';

const router = createRouter({
  history: createWebHashHistory(),
  routes,
})

router.beforeResolve((to, from, next) => {
  if (to.matched.every(route => route.meta.allowAnonymous)) {
    next();
    return;
  }

  if (!authenticationService.hasAuthCookie()) {
    next({ name: 'login' });
    return;
  }

  next();
});

router.afterEach((to, from, next) => {
  if (to.meta && to.meta.title) {
    document.title = `${to.meta.title as string} | ToDoer`;
  } else {
    document.title = 'ToDoer'
  }
});

export default router
