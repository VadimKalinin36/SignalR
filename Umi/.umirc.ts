import { defineConfig } from '@umijs/max';

export default defineConfig({
  antd: {},
  access: {},
  model: {},
  initialState: {},
  request: {},

  routes: [
    {
      path: '/',
      component: './index',
    },
    {
      path: '/auth',
      component: './auth',
    },

    {
      path: '/docs',
      component: './docs',
    },
    {
      path: '/direction',
      component: './direction',
    },
    {
      path: '/students',
      component: './students',
    },
    {
      path: '/country',
      component: './country',
    },    
    {
      path: '/create',
      component: './create',
    },
    {
      path: '/create_direction',
      component: './create_direction',
    },
    {
      path: '/create_student',
      component: './create_student',
    },
    {
      path: '/create_country',
      component: './create_country',
    },
    {
      path: '/edit/:id',
      component: './edit/[id]',
    },
    {
      path: '/edit_direction/:id',
      component: './edit_direction/[id]',
    },
    {
      path: '/edit_country/:id',
      component: './edit_country/[id]',
    },
    {
      path: '/edit_student/:id',
      component: './edit_student/[id]',
    },
    {
      path: '/userEdit',
      component: './userEdit',
    },
    {
      path: '/userEdit2',
      component: './userEdit2',
    },

  ],

  npmClient: 'npm',
});
