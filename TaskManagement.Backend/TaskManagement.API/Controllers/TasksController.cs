﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Requests;
using TaskManagement.Core.Responses;

namespace TaskManagement.API.Controllers
{
    //TODO: add security to the controller (authorize)
    public class TasksController : ODataController
    {
        //TODO: logs
        private readonly ILogger<TasksController> _logger;
        //if the application was bigger, we would have a service layer
        private readonly ITasksRepository _tasksRepository;
        private readonly ITaskSummaryGenerator _taskSummaryGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUpdateTaskRequest> _requestValidator;

        public TasksController(ILogger<TasksController> logger, ITasksRepository tasksService, ITaskSummaryGenerator taskSummaryGenerator,
          IMapper mapper, IValidator<CreateUpdateTaskRequest> requestValidator)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tasksRepository = tasksService ?? throw new ArgumentNullException(nameof(tasksService));
            _taskSummaryGenerator = taskSummaryGenerator ?? throw new ArgumentNullException(nameof(taskSummaryGenerator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        }
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            var task = await _tasksRepository.GetTask(key);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [EnableQuery]
        public IQueryable<TaskEntity> Get()
        {
            return _tasksRepository.GetAllTasks();
        }


        [HttpGet("odata/Tasks/Summary/{key}")]
        public async Task<IActionResult> GetSummary([FromRoute] string key)
        {
            var task = await _tasksRepository.GetTask(key);

            if (task == null)
            {
                return NotFound();
            }

            var summary = await _taskSummaryGenerator.GetTaskDescription();

            return Ok(new TaskWithSummaryResponse
            {
                TaskId = task.Id,
                Summary = summary
            });
        }

        public async Task<IActionResult> Post([FromBody] CreateUpdateTaskRequest createRequest)
        {
            var validationResult = _requestValidator.Validate(createRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(";", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var taskEntity = _mapper.Map<TaskEntity>(createRequest);

            var result = await _tasksRepository.CreateTask(taskEntity);
            if (!result)
            {
                return BadRequest();
            }

            return Created(taskEntity);
        }

        public async Task<IActionResult> Put([FromRoute] string key,
            [FromBody] CreateUpdateTaskRequest updateRequest)
        {
            var validationResult = _requestValidator.Validate(updateRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(";", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var taskEntity = await _tasksRepository.GetTask(key);

            if (taskEntity is null)
            {
                return NotFound();
            }

            taskEntity.Name = updateRequest.Name;
            taskEntity.Description = updateRequest.Description;
            taskEntity.Status = updateRequest.Status;

            await _tasksRepository.UpdateTask(taskEntity);

            return Updated(taskEntity);
        }

        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            var taskEntity = await _tasksRepository.GetTask(key);

            if (taskEntity is not null)
            {
                await _tasksRepository.DeleteTask(taskEntity);
            }

            return NoContent();
        }
    }
}
