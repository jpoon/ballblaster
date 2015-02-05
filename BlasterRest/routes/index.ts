/*
 * GET home page.
 */
import express = require('express');

export function index(req: express.Request, res: express.Response) {
    res.json(200, { title: 'Express' });
};