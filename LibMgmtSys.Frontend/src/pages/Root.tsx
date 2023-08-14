import React from 'react';
import Header from '../components/Header';
import { Outlet } from 'react-router-dom';
import useAppSelector from '../hooks/useAppSelector';
import useAppDispatch from '../hooks/useAppDispatch';

const Root = () => {

  return (
    <>
      <Header />
      <Outlet />
    </>
  );
};

export default Root;
